using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Time
{
	[Serializable]
	public class Clock
	{
		[SerializeField]
		private float currentTime;

		[SerializeField]
		private List<Timer> currentTimers = new();

		public event Action<float> ClockUpdate;

		public void AddTimer(Timer newTimer)
		{
			currentTimers.Add(newTimer);
		}

		public void RemoveTimer(Timer timer)
		{
			currentTimers.Remove(timer);
		}

		public void UpdateClock(float deltaTime)
		{
			for (int index = currentTimers.Count - 1; index >= 0; index--)
			{
				var timer = currentTimers[index];
				try
				{
					timer.Update(deltaTime);
					if (timer.Completed)
						currentTimers.Remove(timer);
				}
				catch (Exception e)
				{
					Debug.LogError(e);
				}
			}

			currentTime += deltaTime;

			ClockUpdate?.Invoke(deltaTime);
		}

		public void Reset()
		{
			currentTimers.Clear();
			currentTime = 0f;
			ClockUpdate = null;
		}
	}
}