using Gameplay.Player.Ability;
using UnityEngine;

namespace Gameplay.Player.Utility
{
	[CreateAssetMenu(menuName = UtilitiesPath + "Dash")]
	public class Dash : BaseAbility
	{
		public float speedMultiplier = 5;
		public float duration = .3f;

		public override void Perform(PlayerBase caster)
		{
			caster.MovementController.Dash(speedMultiplier, duration);
		}
	}
}