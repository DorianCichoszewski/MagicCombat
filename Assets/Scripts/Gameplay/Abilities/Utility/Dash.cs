using Gameplay.Player;
using Gameplay.Player.Ability;
using UnityEngine;

namespace Gameplay.Abilities.Utility
{
	[CreateAssetMenu(menuName = UtilitiesPath + "Dash")]
	public class Dash : BaseAbility
	{
		public float speedMultiplier = 5;
		public float duration = .3f;

		public override void Perform(PlayerBase caster, AbilityState state)
		{
			caster.MovementController.Dash(speedMultiplier, duration);
			state.onPerform?.Invoke();
			state.onFinished?.Invoke();
		}
	}
}