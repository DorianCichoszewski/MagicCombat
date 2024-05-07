using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MagicCombat.Shared.Time
{
	public class ClockManager
	{
		private readonly Clock dynamicClock = new();
		private readonly Clock fixedClock = new();

		[ShowInInspector]
		public Clock DynamicClock => dynamicClock;
		[ShowInInspector]
		public Clock FixedClock => fixedClock;

		public void Reset()
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