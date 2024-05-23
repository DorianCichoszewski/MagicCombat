using System;
using MagicCombat.Shared.TimeSystem;

namespace MagicCombat.Gameplay.Abilities.Limiters
{
	[Serializable]
	public class CooldownLimiter : ILimiter
	{
		public float duration = 1f;

		private Clock clock;

		public CountdownTimer Timer { get; private set; }

		public float RemainingTime => Timer?.RemainingTime ?? duration;
		public float RemainingPercent => Timer?.RemainingPercent ?? 1f;

		public bool CanPerform()
		{
			return Timer == null || Timer.IsFinished;
		}

		public void Start()
		{
			Timer =
				clock.CreateTimer(EndCooldown, duration, $"Cooldown {duration}s") as CountdownTimer;
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
				clock = abilitiesContext.AbilitiesClock
			};
		}

		private void EndCooldown() { }
	}
}