// https://github.com/adammyhre/3D-Platformer

using UnityEngine;
using UnityEngine.Events;

namespace Shared.Notification
{
	public abstract class EventListener<T> : MonoBehaviour
	{
		[SerializeField]
		private EventChannel<T> eventChannel;

		[SerializeField]
		private UnityEvent<T> unityEvent;

		protected void Awake()
		{
			eventChannel.Register(this);
		}

		protected void OnDestroy()
		{
			eventChannel.Deregister(this);
		}

		public void Raise(T value)
		{
			unityEvent?.Invoke(value);
		}
	}

	public class EventListener : EventListener<Empty> { }
}