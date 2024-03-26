using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MagicCombat.Gameplay.Abilities.Limiters
{
	[Serializable]
	[InlineProperty]
	public class LimiterProvider
	{
		[SerializeReference]
		private ILimiter limiterData = new NoneLimiter();

		public ILimiter Limiter(AbilitiesContext abilitiesContext)
		{
			return limiterData.Copy(abilitiesContext);
		}

		public void Init()
		{
			limiterData.Reset();
		}

		public bool CanPerform()
		{
			return limiterData.CanPerform();
		}

		public void Start()
		{
			limiterData.Start();
		}

		public void Reset()
		{
			limiterData.Reset();
		}
	}
}