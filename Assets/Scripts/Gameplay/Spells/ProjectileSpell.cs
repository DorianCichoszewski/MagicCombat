using System;
using Extension;
using Gameplay.Player;
using UnityEngine;
using Timer = Gameplay.Time.Timer;

namespace Gameplay.Spells
{
	public class ProjectileSpell : MonoBehaviour
	{
		[SerializeField]
		private Rigidbody rb;

		[SerializeField]
		private new MeshRenderer renderer;

		private PlayerBase caster;

		private Timer timer;

		public Transform Transform => transform;

		public event Action<PlayerBase> onPlayerHit;
		public event Action<GameObject> onNonPlayerHit;
		public event Action onTimerEnd;

		public void Init(PlayerBase caster, Vector2 direction, float speed, float duration)
		{
			this.caster = caster;
			renderer.material = caster.data.material;

			rb.velocity = direction.ToVec3().normalized * speed;

			timer = new Timer("Projectile", duration, onTimerEnd, caster.GameplayGlobals.clockManager);
		}
		
		private void OnTriggerEnter(Collider other)
		{
			var otherPlayer = other.GetComponentInParent<PlayerBase>();

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

		public void Destroy()
		{
			Destroy(gameObject);
		}
	}
}