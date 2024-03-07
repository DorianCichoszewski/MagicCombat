using System;
using MagicCombat.Extension;
using MagicCombat.Gameplay.Player;
using UnityEngine;
using Timer = MagicCombat.Gameplay.Time.Timer;

namespace MagicCombat.Gameplay.Spells
{
	public class ProjectileSpell : MonoBehaviour
	{
		[SerializeField]
		private Rigidbody rb;

		[SerializeField]
		private new MeshRenderer renderer;

		private PlayerAvatar caster;

		private Timer timer;

		public Transform Transform => transform;

		private void OnTriggerEnter(Collider other)
		{
			var otherPlayer = other.GetComponentInParent<PlayerAvatar>();

			if (otherPlayer != null)
			{
				if (otherPlayer == caster) return;

				onPlayerHit?.Invoke(otherPlayer);
			}
			else
			{
				onNonPlayerHit?.Invoke(other.gameObject);
			}
		}

		public event Action<PlayerAvatar> onPlayerHit;
		public event Action<GameObject> onNonPlayerHit;
		public event Action onTimerEnd;

		public void Init(PlayerAvatar caster, Vector2 direction, float speed, float duration)
		{
			this.caster = caster;
			renderer.material = caster.PlayerController.InitData.material;

			rb.velocity = direction.ToVec3().normalized * speed;

			timer = new Timer("Projectile", duration, onTimerEnd, caster.GameplayGlobals.clockManager);
		}

		public void Destroy()
		{
			timer.Cancel();
			Destroy(gameObject);
		}
	}
}