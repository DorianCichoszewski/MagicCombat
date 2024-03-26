using System;
using MagicCombat.Gameplay.Abilities.Base;
using MagicCombat.Gameplay.Abilities.Limiters;
using UnityEngine;
using UnityEngine.UI;

namespace MagicCombat.Gameplay.UI.Ability
{
	public class AbilityIndicator : MonoBehaviour
	{
		[SerializeField]
		public Image abilityIcon;

		[SerializeField]
		private AbilityIndicatorInputFeedback inputFeedback;

		[Header("limiter Indicators")]
		[SerializeField]
		private CooldownLimiterIndicator cooldownLimiterIndicator;

		public void SetupAbilityCaster(AbilityCaster caster)
		{
			caster.State.onPerform += inputFeedback.AbilityPerformed;
			caster.State.onFailedPerform += inputFeedback.AbilityFailedPerformed;
			caster.State.onStateChanged += OnStateChanged;

			abilityIcon.sprite = caster.State.icon;

			inputFeedback.SetupTweens();
			SetupLimiterIndicator(caster.Limiter);
		}

		private void SetupLimiterIndicator(ILimiter limiter)
		{
			switch (limiter)
			{
				case ChargesLimiter chargesLimiter:
					break;
				case CooldownLimiter cooldownLimiter:
					cooldownLimiterIndicator.AssignLimiter(cooldownLimiter);
					break;
				case NoneLimiter noneLimiter:
					// Nothing
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(limiter));
			}
		}

		private void OnStateChanged(AbilityState state)
		{
			abilityIcon.sprite = state.icon;
		}
	}
}