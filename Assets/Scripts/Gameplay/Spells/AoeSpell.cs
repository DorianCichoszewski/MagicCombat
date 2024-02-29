using System;
using Extension;
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

		public void Init(PlayerAvatar caster, float range, float duration = 0)
		{
			transform.localScale = new Vector3(range, range, range);
			renderer.material = caster.PlayerController.Data.material;
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

		public void Explode(Action<PlayerAvatar, Vector2> onPlayerInExplosion)
		{
			var hits = Physics.SphereCastAll(transform.position, range, Vector3.up);
			foreach (var hit in hits)
			{
				var player = hit.collider.GetComponentInParent<PlayerAvatar>();
				if (player != null)
				{
					var distance = hit.transform.position - transform.position;
					onPlayerInExplosion(player, distance.ToVec2());
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