using Gameplay.Abilities;
using MagicCombat.Gameplay.Limiters;
using UnityEngine.InputSystem;

namespace MagicCombat.Gameplay.Player.Ability
{
	public class AbilityCaster
	{
		private readonly PlayerAvatar caster;

		public AbilityCaster(PlayerAvatar player, BaseAbility ability, GameplayGlobals gameplayGlobals)
		{
			caster = player;
			Ability = ability;
			State = new AbilityState();
			Limiter = null;

			Limiter = ability.limiterProvider.Limiter(gameplayGlobals);
			Limiter.Reset();
			State.onFinished += Limiter.Start;
		}

		public BaseAbility Ability { get; }

		public AbilityState State { get; }

		public ILimiter Limiter { get; }

		public void PerformFromInput(InputAction.CallbackContext ctx)
		{
			if (!ctx.performed) return;

			TryToPerform();
		}

		private void TryToPerform()
		{
			if (Limiter.CanPerform())
			{
				if (State.isActive)
					State.onNextClick();
				else
					Ability.Perform(caster, State);
			}
			else
			{
				State.onFailedPerform?.Invoke();
			}
		}
	}
}