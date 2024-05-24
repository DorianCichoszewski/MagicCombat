// https://github.com/adammyhre/3D-Platformer

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Shared.Notification
{
	public class EventChannel<T> : ScriptableObject
	{
		public event Action<T> OnRaised = delegate { };
		private readonly HashSet<EventListener<T>> monoObservers = new();

		public void Invoke(T value)
		{
			OnRaised(value);
			foreach (var observer in monoObservers)
			{
				observer.Raise(value);
			}
		}

		public void Register(EventListener<T> observer) => monoObservers.Add(observer);
		public void Deregister(EventListener<T> observer) => monoObservers.Remove(observer);
	}

	public readonly struct Empty { }

	[CreateAssetMenu(menuName = "Events/Base Event Channel")]
	public class EventChannel : EventChannel<Empty> { }
}