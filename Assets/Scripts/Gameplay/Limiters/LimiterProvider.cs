using System;
using Sirenix.OdinInspector;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
#endif

namespace Gameplay.Limiters
{
	[Serializable]
	public class LimiterProvider
	{
		[OnValueChanged("@limiterData = GetLimiterFromType($value)")]
		public LimiterType limiterType;
		
		[SerializeReference]
		private ILimiter limiterData;

		public ILimiter Limiter => limiterData.Copy();

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