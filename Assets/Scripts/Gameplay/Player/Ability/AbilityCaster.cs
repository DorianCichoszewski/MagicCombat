using Gameplay.Abilities;
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

		public PlayerBase Player { get; private set; }

		public AbilityState State { get; private set; }

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
			this.Player = player;
			State = new AbilityState();
			limiter = null;
			SetupLimiter();
		}

		private void SetupLimiter()
		{
			if (limiter != null) return;
			limiter = ability.limiterProvider.Limiter;
			limiter.Reset();
			State.onFinished += limiter.Start;
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
				if (State.isActive)
					State.onNextClick();
				else
					ability.Perform(caster, State);
			}
			else
			{
				State.onFailedPerform?.Invoke();
			}
		}
	}
}