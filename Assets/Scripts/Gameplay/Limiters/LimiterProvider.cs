using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MagicCombat.Gameplay.Limiters
{
	[Serializable]
	[InlineProperty]
	public class LimiterProvider
	{
		[OnValueChanged("@limiterData = GetLimiterFromType($value)")]
		public LimiterType limiterType = LimiterType.None;

		[SerializeReference]
		private ILimiter limiterData = new NoneLimiter();

		public ILimiter Limiter(GameplayGlobals gameplayGlobals)
		{
			return limiterData.Copy(gameplayGlobals);
		}

		public void Init()
		{
			limiterData = GetLimiterFromType(limiterType);
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

		public static ILimiter GetLimiterFromType(LimiterType type)
		{
			return type switch
			{
				LimiterType.None => new NoneLimiter(),
				LimiterType.Cooldown => new CooldownLimiter(),
				LimiterType.Charges => new ChargesLimiter(),
				_ => throw new ArgumentOutOfRangeException()
			};
		}
	}

	[Serializable]
	public enum LimiterType
	{
		Cooldown,
		Charges,
		None
	}
}