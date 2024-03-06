using Extension;
using Gameplay.Player;
using Gameplay.Player.Ability;
using Gameplay.Player.Basic;
using Gameplay.Player.Movement;
using Gameplay.Spells;
using UnityEngine;

namespace Gameplay.Abilities
{
	[CreateAssetMenu(menuName = AbilitiesPath + Name, fileName = Name)]
	public class PushProjectile : BaseAbility
	{
		const string Name = "Push Projectile";
		
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
			var createPosition = casterTransform.position + direction.ToVec3() * createOffset + Vector3.up * createOffset;

			var spell = Instantiate(projectile, createPosition, casterTransform.rotation);
			spell.gameObject.name = Name;
			spell.gameObject.transform.localScale = new Vector3(scale, scale, scale);
			spell.onPlayerHit += player =>
			{
				Vector2 pushDirection = (player.transform.position - spell.transform.position).ToVec2().normalized;
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