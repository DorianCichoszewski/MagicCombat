using Gameplay.Player;
using Gameplay.Player.Ability;
using Gameplay.Player.Movement;
using UnityEngine;

namespace Gameplay.Abilities.Utility
{
	[CreateAssetMenu(menuName = UtilitiesPath + "Dash")]
	public class Dash : BaseAbility
	{
		public float speedMultiplier = 5;
		public float duration = .3f;

		public override void Perform(PlayerAvatar caster, AbilityState state)
		{
			var dash = new DashMovement(speedMultiplier, duration);
			caster.MovementController.SetMovement(dash);
			state.onPerform?.Invoke();
			state.onFinished?.Invoke();
		}
	}
}