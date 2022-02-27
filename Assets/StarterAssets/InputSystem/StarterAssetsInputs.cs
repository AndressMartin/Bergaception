using System;
using UnityEngine;
using UnityEngine.Events;
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
		public bool teclaK;
		public bool interact;
		public bool dropItem;

		//Throwing action
		public InputActionReference throwActionReference;
		public UnityEvent throwStarted;
		public UnityEvent throwPerformed;
		public UnityEvent throwCanceled;

		[Header("Movement Settings")]
		public bool analogMovement;

		#region movementEvents

		[Header("Booleans and events for tutorial")]
		public bool wFMovement;
		public bool wFJump;
		public bool wFInteraction;
		public bool wFMouse;
		public VoidEvent fireMove;
		public VoidEvent fireJump;
		public VoidEvent fireInteraction;
		public VoidEvent fireMouse;

		public void SetMovement()
		{
			wFMovement = true;
		}
		public void SetJump()
		{
			wFJump = true;
		}
		public void SetInteract()
		{
			wFInteraction = true;
		}
		public void SetMouse()
		{
			wFMouse = true;
		}
		#endregion


#if !UNITY_IOS || !UNITY_ANDROID
		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

		public void SetCursorLocked(bool boo)
        {
			cursorLocked = boo;
        }
#endif

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED

        public void SetThrowActions()
        {
			throwActionReference.action.started += context =>
			{
				throwStarted.Invoke();
			};
			throwActionReference.action.performed += context =>
			{
				throwPerformed.Invoke();
				FindObjectOfType<AudioManager>().Play("Throw");
			};
			throwActionReference.action.canceled += context =>
			{
				throwCanceled.Invoke();
			};
        }

        public void OnMove(InputValue value)
		{
			if (value.Get<Vector2>() != Vector2.zero && wFMovement)
			{
				Debug.Log("Pressed Move");
				fireMove.Raise();
				wFMovement = false;
			}
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
			if (value.isPressed && wFJump)
			{
				fireJump.Raise();
				Debug.Log("Pressed Jump");
				wFJump = false;
			}
			JumpInput(value.isPressed);
		}

		public void OnAttack(InputValue value)
        {
			if (value.isPressed && wFMouse)
			{
				fireMouse.Raise();
				Debug.Log("Pressed Attack");
				wFMouse = false;
			}
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

		public void OnInteract(InputValue value)
        {
            if (value.isPressed && wFInteraction)
            {
				Debug.Log("Pressed interact");
				fireInteraction.Raise();
				wFInteraction = false;

			}
			InteractInput(value.isPressed);
        }
		public void OnDropItem(InputValue value)
		{
			DropInput(value.isPressed);
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

		public void TeclaKInput(bool newTeclaKState)
        {
			teclaK = newTeclaKState;
        }

		public void InteractInput(bool newInteractState)
        {
			interact = newInteractState;
        }

		private void DropInput(bool newDropState)
		{
			dropItem = newDropState;
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