using System;
using System.Globalization;
using UnityEngine;

namespace Gameplay.Time
{
	[Serializable]
	public class Timer
	{
		[SerializeField]
		private string name;

		[SerializeField]
		private float totalTime;

		[SerializeField]
		private float remainingTime;

		// debug
		private ClockType clockType;
		
		public string Name => name;
		public float TotalTime => totalTime;
		public float RemainingTime => remainingTime;
		public float RemainingPercent => remainingTime / totalTime;
		public bool Completed => remainingTime <= 0;

		public event Action callback;

		public Timer(float time, Action callback, ClockManager manager, ClockType clockUpdateType = ClockType.Dynamic) : this(
			time.ToString(CultureInfo.InvariantCulture), time, callback, manager, clockUpdateType) { }

		public Timer(string name, float time, Action callback, ClockManager manager,ClockType clockUpdateType = ClockType.Dynamic)
		{
			this.name = name;
			totalTime = time;
			remainingTime = time;
			this.callback += callback;
			clockType = clockUpdateType;

			manager.GetClock(clockType).AddTimer(this);
		}

		public void Update(float deltaTime)
		{
			remainingTime -= deltaTime;

			if (Completed)
			{
				callback?.Invoke();
				callback = null;
			}
		}

		public void Cancel()
		{
			callback = null;
			remainingTime = 0;
		}
	}
}