using MagicCombat.Gameplay.Abilities.Limiters;
using MagicCombat.Gameplay.Avatar;

namespace MagicCombat.Gameplay.Abilities.Base
{
	public class AbilityCaster
	{
		public AbilityCaster(BaseAvatar avatar, BaseAbility ability, AbilitiesContext abilitiesContext)
		{
			AbilitiesContext = abilitiesContext;
			Avatar = avatar;
			Ability = ability;
			State = new AbilityState();

			Limiter = ability.limiterProvider.Limiter(abilitiesContext);
			Limiter.Reset();
			State.onFinished += Limiter.Start;
			State.onFinished += Reset;
			Reset();
		}

		public BaseAbility Ability { get; }
		public AbilityState State { get; }
		public ILimiter Limiter { get; }
		public AbilitiesContext AbilitiesContext { get; }

		public BaseAvatar Avatar { get; }

		public void TryToPerform()
		{
			if (Limiter.CanPerform())
			{
				if (State.isActive)
					State.onNextClick?.Invoke();
				else
					Ability.Perform(this, State);
			}
			else
			{
				State.onFailedPerform?.Invoke();
			}
		}

		public void Reset()
		{
			State.icon = Ability.DefaultIcon;
			State.isActive = false;
		}
	}
}