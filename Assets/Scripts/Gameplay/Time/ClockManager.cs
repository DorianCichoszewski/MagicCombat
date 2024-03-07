using System;
using UnityEngine;

namespace MagicCombat.Gameplay.Time
{
	[Serializable]
	public class ClockManager
	{
		[SerializeField]
		private Clock dynamicClock;

		[SerializeField]
		private Clock fixedClock;

		public Clock DynamicClock => dynamicClock;
		public Clock FixedClock => fixedClock;

		public void Init()
		{
			dynamicClock.Reset();
			fixedClock.Reset();
		}

		public Clock GetClock(ClockType type)
		{
			return type switch
			{
				ClockType.Dynamic => DynamicClock,
				ClockType.Fixed => FixedClock,
				_ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
			};
		}

		public void UnityUpdate(float deltaTime)
		{
			dynamicClock.UpdateClock(deltaTime);
		}

		public void UnityFixedUpdate(float fixedDeltaTime)
		{
			fixedClock.UpdateClock(fixedDeltaTime);
		}
	}

	public enum ClockType
	{
		Dynamic,
		Fixed
	}
}