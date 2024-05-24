using System;
using Shared.TimeSystem.Timers;

namespace Shared.TimeSystem
{
	internal static class TimerFactory
	{
		public static Timer CreateTimer(Clock clock, TimerType type, float value)
		{
			var timer = GetTimer(type, value);
			clock.SetupTimer(timer);

			return timer;
		}

		private static Timer GetTimer(TimerType type, float value)
		{
			return type switch
			{
				TimerType.Countdown => new CountdownTimer(value),
				TimerType.Frequency => new FrequencyTimer(value),
				_ => throw new OverflowException()
			};
		}
	}
}