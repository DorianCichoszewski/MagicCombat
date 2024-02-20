using System;
using Gameplay.Limiters;
using Gameplay.Player.Ability;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI.Ability
{
	public class AbilityIndicator : MonoBehaviour
	{
		[SerializeField]
		public Image abilityIcon;
		[SerializeField]
		private AbilityIndicatorInputFeedback inputFeedback;
		
		[Space]
		[SerializeField]
		private CooldownLimiterIndicator cooldownLimiterIndicator;
		
		public void SetupAbilityCaster(AbilityCaster caster)
		{
			caster.TriedPerform += inputFeedback.AbilityTriedPerform;
			
			var ability = caster.ability;

			abilityIcon.sprite = ability.icon;
			
			inputFeedback.SetupTweens();
			SetupLimiterIndicator(ability.limiterProvider.limiterType, caster.Limiter);
		}

		private void SetupLimiterIndicator(LimiterType type, ILimiter limiter)
		{
			switch (type)
			{
				case LimiterType.Cooldown:
					cooldownLimiterIndicator.AssignLimiter((CooldownLimiter)limiter);
					break;
				case LimiterType.Charges:
					break;
				case LimiterType.None:
					// Nothing
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(type), type, null);
			}
		}
	}
}