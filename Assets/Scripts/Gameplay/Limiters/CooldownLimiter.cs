using System;
using MagicCombat.Gameplay.Time;

namespace MagicCombat.Gameplay.Limiters
{
	[Serializable]
	public class CooldownLimiter : ILimiter
	{
		public float duration = 1f;

		private GameplayGlobals gameplayGlobals;

		public Timer Timer { get; private set; }

		public float RemainingTime => Timer?.RemainingTime ?? 0f;
		public float RemainingPercent => Timer?.RemainingPercent ?? 0f;

		public bool CanPerform()
		{
			return Timer == null || Timer.Completed;
		}

		public void Start()
		{
			Timer = new Timer($"Cooldown {duration}s", duration, EndCooldown, gameplayGlobals.clockManager);
		}

		public void Reset()
		{
			Timer = null;
		}

		public ILimiter Copy(GameplayGlobals gameplayGlobals)
		{
			return new CooldownLimiter
			{
				duration = duration,
				gameplayGlobals = gameplayGlobals
			};
		}

		private void EndCooldown() { }
	}
}