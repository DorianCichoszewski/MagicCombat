using Extension;
using Gameplay.Player;
using Gameplay.Time;
using UnityEngine;

namespace Gameplay
{
	public class Projectile : MonoBehaviour
	{
		[SerializeField]
		private Rigidbody rb;

		[SerializeField]
		private new MeshRenderer renderer;

		private PlayerBase caster;

		private Timer timer;

		private void OnTriggerEnter(Collider other)
		{
			var otherPlayer = other.GetComponentInParent<PlayerBase>();

			if (otherPlayer == caster) return;

			Explode();
		}

		public void Init(PlayerBase caster, Vector2 direction, float speed, float duration)
		{
			this.caster = caster;
			renderer.material = caster.data.material;

			rb.velocity = direction.ToVec3().normalized * speed;

			timer = new Timer("Projectile", duration, Explode);
		}

		private void Explode()
		{
			timer.callback -= Explode;
			Destroy(gameObject);
		}
	}
}