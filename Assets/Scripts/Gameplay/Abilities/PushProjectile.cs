using Gameplay.Abilities;
using Gameplay.Player.Movement;
using MagicCombat.Extension;
using MagicCombat.Gameplay.Player;
using MagicCombat.Gameplay.Player.Ability;
using MagicCombat.Gameplay.Spells;
using UnityEngine;

namespace MagicCombat.Gameplay.Abilities
{
	[CreateAssetMenu(menuName = AbilitiesPath + Name, fileName = Name)]
	public class PushProjectile : BaseAbility
	{
		private const string Name = "Push Projectile";

		[Space]
		public ProjectileSpell projectile;

		public float createOffset = 1;
		public float scale = 1;

		public float speed = 20;
		public float duration = 3;
		public float pushStrength = 20f;
		public MovementForce pushForce;

		public override void Perform(PlayerAvatar caster, AbilityState state)
		{
			var casterTransform = caster.transform;
			var direction = caster.MovementController.LookDirection;
			var createPosition = casterTransform.position + direction.ToVec3() * createOffset +
								 Vector3.up * createOffset;

			var spell = Instantiate(projectile, createPosition, casterTransform.rotation);
			spell.gameObject.name = Name;
			spell.gameObject.transform.localScale = new Vector3(scale, scale, scale);
			spell.onPlayerHit += player =>
			{
				var pushDirection = (player.transform.position - spell.transform.position).ToVec2().normalized;
				player.MovementController.AddForce(pushForce.GetNew(pushDirection * pushStrength));
				spell.Destroy();
			};
			spell.onNonPlayerHit += _ => spell.Destroy();
			spell.onTimerEnd += spell.Destroy;
			spell.Init(caster, direction, speed, duration);
			state.onPerform?.Invoke();
			state.onFinished?.Invoke();
		}
	}
}