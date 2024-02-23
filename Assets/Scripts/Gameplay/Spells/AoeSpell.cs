using System;
using Gameplay.Player;
using UnityEngine;

namespace Gameplay.Spells
{
	public class AoeSpell : MonoBehaviour
	{
		[SerializeField]
		private new MeshRenderer renderer;

		[SerializeField]
		private new Collider collider;

		private float range;

		//public event Action<PlayerBase> onPlayerEnter;
		//public event Action<PlayerBase> onPlayerExit;

		public void Init(PlayerBase caster, float range, float duration = 0)
		{
			transform.localScale = new Vector3(range, range, range);
			renderer.material = caster.data.material;
			this.range = range;

			if (duration > 0)
			{
				// TODO add lasting AOE effect
			}
			else
			{
				collider.enabled = false;
			}
		}

		public void Explode(Action<PlayerBase, Vector2> onPlayerInExplosion)
		{
			var hits = Physics.SphereCastAll(transform.position, range, Vector3.up);
			foreach (var hit in hits)
			{
				var player = hit.collider.GetComponentInParent<PlayerBase>();
				if (player != null)
				{
					var distance = hit.transform.position - transform.position;
					distance.y = 0f;
					onPlayerInExplosion(player, distance);
				}
			}

			Invoke(nameof(Destroy), 0.05f);
		}

		private void Destroy()
		{
			Destroy(gameObject);
		}
	}
}