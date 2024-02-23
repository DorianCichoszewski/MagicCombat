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

		private Timer timer;

		public Timer Timer => timer;

		public float RemainingTime => timer?.RemainingTime ?? 0f;
		public float RemainingPercent => timer?.RemainingPercent ?? 0f;

		public bool CanPerform()
		{
			return timer == null || timer.Completed;
		}

		public void Start()
		{
			timer = new Timer(duration, EndCooldown, gameplayGlobals.clockManager);
		}

		public void Reset()
		{
			timer = null;
		}

		public ILimiter Copy()
		{
			return new CooldownLimiter
			{
				duration = duration,
				gameplayGlobals = gameplayGlobals,
			};
		}

		private void EndCooldown() { }
	}
}