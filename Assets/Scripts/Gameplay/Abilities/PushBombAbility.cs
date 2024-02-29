using Extension;
using Gameplay.Player;
using Gameplay.Player.Ability;
using Gameplay.Player.Basic;
using Gameplay.Spells;
using UnityEngine;

namespace Gameplay.Abilities
{
	[CreateAssetMenu(menuName = AbilitiesPath + Name, fileName = Name)]
	public class PushBombAbility : BaseAbility
	{
		const string Name = "PushBomb";
		
		[Space]
		[SerializeField]
		private ProjectileSpell projectilePrefab;

		[SerializeField]
		private AoeSpell explosionPrefab;

		[SerializeField]
		private Sprite travelingProjectileSprite;
		
		public float projectileSpeed = 20;
		public float projectileDuration = 3;

		[Space]
		public float explosionRange = 5f;

		public float explosionMaxStrength = 10f;
		public float explosionMinStrength = 3f;
		public MovementForce explosionForce;

		public override Sprite GetIcon(AbilityState state)
		{
			return state.isActive ? travelingProjectileSprite : defaultIcon;
		}

		public override void Perform(PlayerAvatar caster, AbilityState state)
		{
			var casterTransform = caster.transform;
			var direction = caster.MovementController.LookDirection;
			var createPosition = casterTransform.position + (direction.ToVec3() + Vector3.up) * 2;

			var projectile = Instantiate(projectilePrefab, createPosition, casterTransform.rotation);
			projectile.Init(caster, direction, projectileSpeed, projectileDuration);
			projectile.onPlayerHit += _ => MakeExplosion(projectile, caster, state);
			projectile.onNonPlayerHit += _ => MakeExplosion(projectile, caster, state);
			projectile.onTimerEnd += () => MakeExplosion(projectile, caster, state);
			state.isActive = true;
			state.onNextClick = () => MakeExplosion(projectile, caster, state);
			state.onStateChanged?.Invoke(state);
			state.onPerform?.Invoke();
		}

		private void MakeExplosion(ProjectileSpell projectile, PlayerAvatar caster, AbilityState state)
		{
			var explosion = Instantiate(explosionPrefab, projectile.transform.position, projectile.transform.rotation);

			projectile.Destroy();
			
			explosion.Init(caster, explosionRange);
			explosion.Explode((player, distance) =>
			{
				float distancePercent = Mathf.InverseLerp(0, explosionRange, distance.magnitude);
				float strength = Mathf.Lerp(explosionMaxStrength, explosionMinStrength, distancePercent);
				Vector2 direction = (player.transform.position - explosion.transform.position).ToVec2().normalized;
				player.MovementController.AddForce(explosionForce.GetNew(direction * strength));
			});
			state.isActive = false;
			state.onStateChanged?.Invoke(state);
			state.onFinished?.Invoke();
		}
	}
}