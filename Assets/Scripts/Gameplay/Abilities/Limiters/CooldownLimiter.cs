using System;
using MagicCombat.Shared.Time;

namespace MagicCombat.Gameplay.Abilities.Limiters
{
	[Serializable]
	public class CooldownLimiter : ILimiter
	{
		public float duration = 1f;

		private ClockManager clockManager;

		public Timer Timer { get; private set; }

		public float RemainingTime => Timer?.RemainingTime ?? 0f;
		public float RemainingPercent => Timer?.RemainingPercent ?? 0f;

		public bool CanPerform()
		{
			return Timer == null || Timer.Completed;
		}

		public void Start()
		{
			Timer = new Timer($"Cooldown {duration}s", duration, EndCooldown, clockManager);
		}

		public void Reset()
		{
			Timer = null;
		}

		public ILimiter Copy(AbilitiesContext abilitiesContext)
		{
			return new CooldownLimiter
			{
				duration = duration,
				clockManager = abilitiesContext.clockManager
			};
		}

		private void EndCooldown() { }
	}
}