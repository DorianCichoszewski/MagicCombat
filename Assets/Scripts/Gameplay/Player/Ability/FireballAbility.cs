using UnityEngine;

namespace Gameplay.Player.Ability
{
	[CreateAssetMenu(menuName = AbilitiesPath + "Fireball")]
	public class FireballAbility : BaseAbility
	{
		public Projectile projectile;

		public float createOffset = 1;
		public float scale = 1;

		public float speed = 20;
		public float duration = 3;

		public override void Perform(PlayerBase caster)
		{
			var casterTransform = caster.transform;
			var direction = casterTransform.forward;
			var createPosition = casterTransform.position + direction * createOffset + Vector3.up * createOffset;

			var spell = Instantiate(projectile, createPosition, casterTransform.rotation);
			spell.gameObject.transform.localScale = new Vector3(scale, scale, scale);
			spell.Init(caster, new Vector2(direction.x, direction.z), speed, duration);
		}
	}
}