using System;
using Gameplay.Time;

namespace Gameplay.Limiters
{
	[Serializable]
	public class CooldownLimiter : ILimiter
	{
		public float duration;

		private Timer timer;

		public Timer Timer => timer;

		public float RemainingTime => timer?.RemainingTime ?? 0f;
		public float RemainingPercent => timer?.RemainingPercent ?? 0f;

		public bool CanPerform()
		{
			return timer == null || timer.Completed;
		}

		public bool TryPerform()
		{
			if (CanPerform())
			{
				timer = new Timer(duration, EndCooldown);
				return true;
			}

			return false;
		}

		public void Reset()
		{
			timer = null;
		}

		public ILimiter Copy()
		{
			return new CooldownLimiter
			{
				duration = duration
			};
		}

		private void EndCooldown() { }
	}
}