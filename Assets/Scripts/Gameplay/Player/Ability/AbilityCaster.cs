using Gameplay.Limiters;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Player.Ability
{
	public class AbilityCaster : MonoBehaviour
	{
		[SerializeField]
		private PlayerBase caster;

		// TMP public
		public BaseAbility ability;
		private ILimiter limiter;
		private PlayerBase player;

		private AbilityState abilityState;

		public PlayerBase Player => player;

		public AbilityState State => abilityState;
		public ILimiter Limiter
		{
			get
			{
				SetupLimiter();
				return limiter;
			}
		}

		public void Init(PlayerBase player)
		{
			this.player = player;
			abilityState = new();
			limiter = null;
			SetupLimiter();
		}

		private void SetupLimiter()
		{
			if (limiter != null) return;
			limiter = ability.limiterProvider.Limiter;
			limiter.Reset();
			abilityState.onFinished += limiter.Start;
		}

		public void PerformFromInput(InputAction.CallbackContext ctx)
		{
			if (!ctx.performed) return;
			
			TryToCast();
		}

		public void TryToCast()
		{
			if (limiter.CanPerform())
			{
				ability.Perform(caster, abilityState);
				abilityState.onPerform?.Invoke();
			}
			else
			{
				abilityState.onFailedPerform?.Invoke();
			}
		}
	}
}