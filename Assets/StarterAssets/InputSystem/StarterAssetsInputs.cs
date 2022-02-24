using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool attack;
		public bool dash;
		public bool throwItem;
		public bool interact;
		public bool dropItem;
		public bool teclaK;

		[Header("Movement Settings")]
		public bool analogMovement;

#if !UNITY_IOS || !UNITY_ANDROID
		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;
#endif

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnAttack(InputValue value)
        {
			AttackInput(value.isPressed);
        }

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}

		public void OnDash(InputValue value)
        {
			DashInput(value.isPressed);
        }

		public void OnTeclaK(InputValue value)
        {
			TeclaKInput(value.isPressed);
        }

		public void OnThrow(InputValue value)
        {
			ThrowInput(value.isPressed);
        }

		public void OnInteractIten(InputValue value)
		{
			Interact(value.isPressed);
		}
		public void OnDropItem(InputValue value)
		{
			DropItem(value.isPressed);
		}
#else
	// old input sys if we do decide to have it (most likely wont)...
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void AttackInput(bool newAttackState)
        {
			attack = newAttackState;
        }

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		public void DashInput(bool newDashState)
        {
			dash = newDashState;
        }

		public void ThrowInput(bool newThrowState)
        {
			throwItem = newThrowState;
        }
		public void Interact(bool newThrowState)
		{
			interact = newThrowState;
		}
		public void DropItem(bool newThrowState)
		{
			dropItem = newThrowState;
		}

		public void TeclaKInput(bool newTeclaKState)
        {
			teclaK = newTeclaKState;
        }

#if !UNITY_IOS || !UNITY_ANDROID

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}

#endif

	}
	
}