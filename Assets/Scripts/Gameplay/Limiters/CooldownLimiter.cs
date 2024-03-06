using System;
using Gameplay.Time;
using UnityEngine;

namespace Gameplay.Limiters
{
	[Serializable]
	public class CooldownLimiter : ILimiter
	{
		[SerializeField]
		private GameplayGlobals gameplayGlobals;

		public float duration;

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

		public ILimiter Copy()
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