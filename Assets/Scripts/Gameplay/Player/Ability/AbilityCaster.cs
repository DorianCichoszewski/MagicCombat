using Gameplay.Abilities;
using Gameplay.Limiters;
using UnityEngine.InputSystem;

namespace Gameplay.Player.Ability
{
	public class AbilityCaster
	{
		private readonly PlayerAvatar caster;
		private readonly BaseAbility ability;
		private readonly ILimiter limiter;
		private readonly AbilityState state;

		public BaseAbility Ability => ability;
		public AbilityState State => state;
		public ILimiter Limiter => limiter;

		public AbilityCaster(PlayerAvatar player, BaseAbility ability)
		{
			caster = player;
			this.ability = ability;
			state = new AbilityState();
			limiter = null;
			
			limiter = ability.limiterProvider.Limiter;
			limiter.Reset();
			State.onFinished += limiter.Start;
		}
		
		public void PerformFromInput(InputAction.CallbackContext ctx)
		{
			if (!ctx.performed) return;

			TryToPerform();
		}

		private void TryToPerform()
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