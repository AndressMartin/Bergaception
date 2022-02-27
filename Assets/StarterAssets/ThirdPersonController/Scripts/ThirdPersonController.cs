using System;
using System.Collections;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

/* Note: animations are called via the controller for both the character and capsule using animator null checks
 */

namespace StarterAssets
{
	[RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
	[RequireComponent(typeof(PlayerInput))]
#endif
	public class ThirdPersonController : MonoBehaviour
	{
		[Header("Player")]
		[Tooltip("Move speed of the character in m/s")]
		public float MoveSpeed = 2.0f;
		[Tooltip("Sprint speed of the character in m/s")]
		public float SprintSpeed = 5.335f;
		[Tooltip("How fast the character turns to face movement direction")]
		[Range(0.0f, 0.3f)]
		public float RotationSmoothTime = 0.12f;
		[Tooltip("Acceleration and deceleration")]
		public float SpeedChangeRate = 10.0f;

		[Space(10)]
		[Tooltip("The height the player can jump")]
		public float JumpHeight = 1.2f;
		[Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
		public float Gravity = -15.0f;

		[Space(10)]
		[Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
		public float JumpTimeout = 0.50f;
		[Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
		public float FallTimeout = 0.15f;

		[Header("Player Grounded")]
		[Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
		public bool Grounded = true;
		[Tooltip("Useful for rough ground")]
		public float GroundedOffset = -0.14f;
		[Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
		public float GroundedRadius = 0.28f;
		[Tooltip("What layers the character uses as ground")]
		public LayerMask GroundLayers;

		[Header("Cinemachine")]
		[Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
		public GameObject CinemachineCameraTarget;
		[Tooltip("How far in degrees can you move the camera up")]
		public float TopClamp = 70.0f;
		[Tooltip("How far in degrees can you move the camera down")]
		public float BottomClamp = -30.0f;
		[Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
		public float CameraAngleOverride = 0.0f;
		[Tooltip("For locking the camera position on all axis")]
		public bool LockCameraPosition = false;

		// cinemachine
		private float _cinemachineTargetYaw;
		private float _cinemachineTargetPitch;

		// player
		private float _speed;
		private float _dashSpeed = 1f;
		private float _animationBlend;
		private float _targetRotation = 0.0f;
		private float _rotationVelocity;
		private float _verticalVelocity;
		private float _terminalVelocity = 53.0f;

		// timeout deltatime
		private float _jumpTimeoutDelta;
		private float _fallTimeoutDelta;

		// animation IDs
		private int _animIDSpeed;
		private int _animIDGrounded;
		private int _animIDJump;
		private int _animIDAttack;
		private int _animIDReaction;
		private int _animIDFreeFall;
		private int _animIDMotionSpeed;
		private int _animIDThrow;
        private int _animIDDying;
		private int _animIDInteract;
		private int _animIDUse;
		private int _animIDDropItem;

        private Animator _animator;
		private CharacterController _controller;
		private StarterAssetsInputs _input;
		private GameObject _mainCamera;
		private Character player;

		private const float _threshold = 0.01f;

		private bool _hasAnimator;
		//Throwing stuff
		[SerializeField]
        private GameObject itemToThrow;
		[SerializeField]
		private DrawProjection projection;
		public Transform ShotPoint;
		public float blastPower;


        private void Awake()
		{
			// get a reference to our main camera
			if (_mainCamera == null)
			{
				_mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
			}
			player = GetComponent<Character>();
			player.recebeuDano += ReactToDamage;
		}

		private void Start()
		{
			_hasAnimator = TryGetComponent(out _animator);
			_controller = GetComponent<CharacterController>();
			_input = GetComponent<StarterAssetsInputs>();

			_input.SetThrowActions();
			_input.throwStarted.AddListener(ThrowStarted);
            _input.throwPerformed.AddListener(ThrowPerformed);
			_input.throwCanceled.AddListener(ThrowCanceled);

			_hasAnimator = TryGetComponent(out _animator);

			AssignAnimationIDs();

			// reset our timeouts on start
			_jumpTimeoutDelta = JumpTimeout;
			_fallTimeoutDelta = FallTimeout;
		}


		#region inputBlockers
		public bool allowMove;
		public bool allowJump;
		public bool allowMouse;
		public bool allowInteract;

		public void SetAllowMove(bool boo)
		{
			allowMove = boo;
		}
		public void SetAllowJump(bool boo)
		{
			allowJump = boo;
		}
		public void SetAllowMouse(bool boo)
		{
			allowMouse = boo;
		}
		public void SetAllowInteract(bool boo)
		{
			allowInteract = boo;
		}
		#endregion

		

		private void Update()
		{

			GroundedCheck();
			if (allowJump) JumpOrDash();
			if(allowMove) Move();
			if (allowMouse) Attack();
			//Throw();
			DropItem();
			ApertarTeclaK();
			Morrer();
			if (allowInteract) Interagir();
		}



        private void LateUpdate()
		{
			CameraRotation();
		}
		public void AllBoolFalse()
        {
			_animator.SetBool(_animIDJump, false);
			_animator.SetBool(_animIDAttack, false);
		}
		private void AssignAnimationIDs()
		{
			_animIDSpeed = Animator.StringToHash("Speed");
			_animIDGrounded = Animator.StringToHash("Grounded");
			_animIDJump = Animator.StringToHash("Jump");
			_animIDFreeFall = Animator.StringToHash("FreeFall");
			_animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
			_animIDAttack = Animator.StringToHash("Attacking");
			_animIDReaction = Animator.StringToHash("Reaction");
			_animIDThrow = Animator.StringToHash("Throw");
			_animIDDying = Animator.StringToHash("Dying");
			_animIDInteract = Animator.StringToHash("Interact");
			_animIDDropItem = Animator.StringToHash("DropItem");
			_animIDUse = Animator.StringToHash("Use");
		}

		private void GroundedCheck()
		{
			// set sphere position, with offset
			Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
			Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);

			// update animator if using character
			if (_hasAnimator)
			{
				_animator.SetBool(_animIDGrounded, Grounded);
			}
		}

		private void CameraRotation()
		{
			// if there is an input and camera position is not fixed
			if (_input.look.sqrMagnitude >= _threshold && !LockCameraPosition)
			{
				_cinemachineTargetYaw += _input.look.x * Time.deltaTime;
				_cinemachineTargetPitch += _input.look.y * Time.deltaTime;
			}

			// clamp our rotations so our values are limited 360 degrees
			_cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
			_cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

			// Cinemachine will follow this target
			CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride, _cinemachineTargetYaw, 0.0f);
		}

		public Vector3 GetDirection()
        {
			return new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;
		}

		private void Move()
		{
			// set target speed based on move speed, sprint speed and if sprint is pressed
			float targetSpeed = _input.sprint ? SprintSpeed : MoveSpeed;
			// a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

				// note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
				// if there is no input, set the target speed to 0
				// if there is an input where no movement should happen, set the target speed to 0

			if (_input.move == Vector2.zero || 
				_animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.EndThrow") ||
				_animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Throw") ||
				_animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Reaction") ||
				_animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.DropItem") ||
				_animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Use") ||
				_animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Dying"))
			{
				targetSpeed = 0.0f;
			}

			// a reference to the players current horizontal velocity
			float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

			float speedOffset = 0.1f;
			float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

			// accelerate or decelerate to target speed
			if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
			{
				// creates curved result rather than a linear one giving a more organic speed change
				// note T in Lerp is clamped, so we don't need to clamp our speed
				_speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);

				// round speed to 3 decimal places
				_speed = Mathf.Round(_speed * 1000f) / 1000f;
			}
			else
			{
				_speed = targetSpeed;
			}
			_animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);

			// normalise input direction
			Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

			// note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
			// if there is a move input rotate player when the player is moving
			if (_input.move != Vector2.zero)
			{
				_targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;
				float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, RotationSmoothTime);
				// rotate to face input direction relative to camera position
				transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
			}


			Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

			// move the player
			_controller.Move(targetDirection.normalized * (_speed * _dashSpeed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
			if (_dashSpeed > 1f)
			{
				_dashSpeed = _dashSpeed - 0.1f;
			}
			else _dashSpeed = 1f;
			// update animator if using character
			if (_hasAnimator)
			{
				_animator.SetFloat(_animIDSpeed, _animationBlend);
				_animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
			}
		}

		private void JumpOrDash()
		{
			if (Grounded)
			{
				// reset the fall timeout timer
				_fallTimeoutDelta = FallTimeout;

				// update animator if using character
				if (_hasAnimator)
				{
					_animator.SetBool("Dash", false);
					_animator.SetBool(_animIDJump, false);
					_animator.SetBool(_animIDFreeFall, false);
				}

				// stop our velocity dropping infinitely when grounded
				if (_verticalVelocity < 0.0f)
				{
					_verticalVelocity = -2f;
				}

				
				if (_input.jump && _jumpTimeoutDelta <= 0.0f)
				{
					// the square root of H * -2 * G = how much velocity needed to reach desired height
						_verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);

					//update animator if using character
					if (_hasAnimator)
					{
						_animator.SetBool(_animIDJump, true);
					}
				}
				else if (_input.dash && _jumpTimeoutDelta <= 0.0f)
                {
					_verticalVelocity = Mathf.Sqrt(JumpHeight * -1.3f * Gravity);
					_dashSpeed = 1.2f;

					if (_hasAnimator)
					{
						_animator.SetBool("Dash", true);
					}
				}

				// jump timeout
				if (_jumpTimeoutDelta >= 0.0f)
				{
					_jumpTimeoutDelta -= Time.deltaTime;
				}
			}
			else
			{
				// reset the jump timeout timer
				_jumpTimeoutDelta = JumpTimeout;

				// fall timeout
				if (_fallTimeoutDelta >= 0.0f)
				{
					_fallTimeoutDelta -= Time.deltaTime;
				}
				else
				{
					// update animator if using character
					if (_hasAnimator)
					{
						_animator.SetBool(_animIDFreeFall, true);
					}
				}

				// if we are not grounded, do not jump
				_input.jump = false;
				_input.dash = false;
			}

			// apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
			if (_verticalVelocity < _terminalVelocity)
			{
				_verticalVelocity += Gravity * Time.deltaTime;
			}
		}

		public void LaunchUpwards()
        {
			Debug.Log("Launching Upwards");
			Grounded = true;
			_input.jump = true;
			JumpOrDash();
			player.ReceberDano(1);
		}

		public void Attack()
        {
			if (_input.attack && !_animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Attack3"))
            {
				_animator.SetBool(_animIDAttack, true);
            }
			_input.attack = false;
		}

		//private void Throw()
		//{
  //          if (_input.throwItem && !_animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Throw"))
  //          {
		//		_animator.SetTrigger(_animIDThrow);
		//	}
		//	_input.throwItem = false;
		//}

		private void ThrowPerformed()
		{
			if (allowMouse) _animator.SetTrigger("EndThrow");
		}

		private void ThrowStarted()
		{
			if(allowMouse) _animator.SetTrigger(_animIDThrow);
		}

		private void ThrowCanceled()
		{
			if (allowMouse) _animator.SetTrigger("CancelThrow");
		}

		public void ThrowItem()
        {
			GameObject CreatedCannonball = Instantiate(itemToThrow, ShotPoint.position, ShotPoint.rotation);
			CreatedCannonball.transform.position = ShotPoint.transform.position;
			CreatedCannonball.transform.rotation = ShotPoint.transform.rotation;
			CreatedCannonball.GetComponent<Rigidbody>().velocity = ShotPoint.transform.up * blastPower;
			Debug.LogError("Test");
		}
		public void DropItem()
		{
			if (_input.dropItem && !_animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.DropItem"))
			{
				_animator.SetTrigger(_animIDDropItem);
				player.DroparItem();
			}
			_input.dropItem = false;
		}

		public void ApertarTeclaK()
        {
			if (_input.teclaK)
            {
				Debug.Log("Oh nao, apertei o K!");
				_input.teclaK = false;
            }
        }

		private void Morrer()
		{
			if(player.Vida <= 0 && !_animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Dying"))
            {
				_animator.SetTrigger(_animIDDying);
            }
		}
		private void Interagir()
		{
			if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Interact") ||
				_animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Use")) return;

			if (_input.interact)
            {
				if (player.TemItem())
                {
					_animator.SetTrigger(_animIDUse);
					player.UsarItem();
                }
                else
                {
					_animator.SetTrigger(_animIDInteract);
					player.InteracaoItem();
				}
			}
			_input.interact = false;
		}

		public void ToggleProjection()
        {
			projection.draw = !projection.draw;
        }

		private void ReactToDamage()
		{
			_animator.SetTrigger(_animIDReaction);
		}
		private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
		{
			if (lfAngle < -360f) lfAngle += 360f;
			if (lfAngle > 360f) lfAngle -= 360f;
			return Mathf.Clamp(lfAngle, lfMin, lfMax);
		}

		private void OnDrawGizmosSelected()
		{
			Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
			Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

			if (Grounded) Gizmos.color = transparentGreen;
			else Gizmos.color = transparentRed;
			
			// when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
			Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z), GroundedRadius);
		}
	}
}