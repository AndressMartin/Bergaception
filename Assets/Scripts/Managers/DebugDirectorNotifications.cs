using UnityEngine;
using UnityEngine.Playables;

namespace PlayableExtensions
{
	public class DebugDirectorNotifications : MonoBehaviour
	{
		//-----------------------------------------------------------------------------------------
		// Inspector Variables
		//-----------------------------------------------------------------------------------------

		[SerializeField] private PlayableDirectorNotifications _notifications = default;

		//-----------------------------------------------------------------------------------------
		// Unity Lifecycle Methods
		//-----------------------------------------------------------------------------------------

		private void OnEnable()
		{
			_notifications.Played += Notifications_Played;
			_notifications.Paused += Notifications_Paused;
			_notifications.Stopped += Notifications_Stopped;
			_notifications.Completed += Notifications_Completed;
			_notifications.StateChanged += Notifications_StateChanged;
			_notifications.Wrapped += Notifications_Wrapped;
		}

		private void OnDisable()
		{
			_notifications.Played -= Notifications_Played;
			_notifications.Paused -= Notifications_Paused;
			_notifications.Stopped -= Notifications_Stopped;
			_notifications.Completed -= Notifications_Completed;
			_notifications.StateChanged -= Notifications_StateChanged;
			_notifications.Wrapped -= Notifications_Wrapped;
		}

		//-----------------------------------------------------------------------------------------
		// Event Handlers
		//-----------------------------------------------------------------------------------------

		private void Notifications_Played() { Debug.Log("Played"); }

		private void Notifications_Paused() { Debug.Log("Paused"); }

		private void Notifications_Stopped() { Debug.Log("Stopped"); }
		
		private void Notifications_Completed() { Debug.Log("Completed"); }

		private void Notifications_StateChanged(PlayableState state) { Debug.Log(state); }

		private void Notifications_Wrapped(DirectorWrapMode wrapMode) { Debug.Log(wrapMode); }
	}
}