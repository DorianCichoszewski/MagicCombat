using MagicCombat.Gameplay.Abilities.Base;
using MagicCombat.Gameplay.Avatar.Movement;
using UnityEngine;

namespace MagicCombat.Gameplay.Abilities.Utility
{
	[CreateAssetMenu(menuName = UtilitiesPath + "Dash")]
	public class Dash : BaseAbility
	{
		public float speedMultiplier = 5;
		public float duration = .3f;

		public override void Perform(AbilityCaster caster, AbilityState state)
		{
			var dash = new DashMovement(speedMultiplier, duration);
			caster.Avatar.MovementController.SetMovement(dash);
			state.onPerform?.Invoke();
			state.onFinished?.Invoke();
		}
	}
}