using System;
using Gameplay.Abilities;
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

		[Header("limiter Indicators")]
		[SerializeField]
		private CooldownLimiterIndicator cooldownLimiterIndicator;

		private BaseAbility ability;

		public void SetupAbilityCaster(AbilityCaster caster)
		{
			caster.State.onPerform += inputFeedback.AbilityPerformed;
			caster.State.onFailedPerform += inputFeedback.AbilityFailedPerformed;
			caster.State.onStateChanged += OnStateChanged;

			ability = caster.Ability;

			abilityIcon.sprite = ability.GetIcon(caster.State);

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

		private void OnStateChanged(AbilityState state)
		{
			abilityIcon.sprite = ability.GetIcon(state);
		}
	}
}