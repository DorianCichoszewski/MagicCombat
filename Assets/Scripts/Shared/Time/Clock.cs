using System;
using System.Collections.Generic;
using UnityEngine;

namespace MagicCombat.Shared.Time
{
	[Serializable]
	public class Clock
	{
		[SerializeField]
		private float currentSpeed = 1;

		[SerializeField]
		private float currentTime;

		[SerializeField]
		private List<Timer> currentTimers = new();

		public float CurrentSpeed
		{
			get => currentSpeed;
			set => currentSpeed = value;
		}

		public float CurrentTime => currentTime;

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
			deltaTime *= currentSpeed;

			// Inverse order to enable removal
			for (int index = currentTimers.Count - 1; index >= 0; index--)
			{
				var timer = currentTimers[index];
				try
				{
					timer.Update(deltaTime);
				}
				catch (Exception e)
				{
					Debug.LogError(e);
				}

				if (timer.Completed)
					currentTimers.Remove(timer);
			}

			currentTime += deltaTime;

			ClockUpdate?.Invoke(deltaTime);
		}

		public void Reset()
		{
			currentTimers.Clear();
			currentTime = 0f;
			currentSpeed = 1f;
			ClockUpdate = null;
		}
	}
}