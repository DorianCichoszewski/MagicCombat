using Extension;
using Gameplay.Player;
using Gameplay.Player.Ability;
using Gameplay.Spells;
using UnityEngine;

namespace Gameplay.Abilities
{
	[CreateAssetMenu(menuName = AbilitiesPath + Name, fileName = Name)]
	public class FireballAbility : BaseAbility
	{
		const string Name = "Fireball";
		
		[Space]
		public ProjectileSpell projectile;

		public float createOffset = 1;
		public float scale = 1;

		public float speed = 20;
		public float duration = 3;

		public override void Perform(PlayerBase caster, AbilityState state)
		{
			var casterTransform = caster.transform;
			var direction = caster.MovementController.LookDirection;
			var createPosition = casterTransform.position + direction.ToVec3() * createOffset + Vector3.up * createOffset;

			var spell = Instantiate(projectile, createPosition, casterTransform.rotation);
			spell.gameObject.transform.localScale = new Vector3(scale, scale, scale);
			spell.Init(caster, direction, speed, duration);
			spell.onPlayerHit += player =>
			{
				player.Hit();
				spell.Destroy();
			};
			spell.onNonPlayerHit += _ => spell.Destroy();
			spell.onTimerEnd += spell.Destroy;
			state.onPerform?.Invoke();
			state.onFinished?.Invoke();
		}
	}
}