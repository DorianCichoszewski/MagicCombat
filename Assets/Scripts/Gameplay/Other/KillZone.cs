using MagicCombat.Gameplay.Avatar;
using UnityEngine;

namespace MagicCombat.Gameplay.Other
{
	public class KillZone : MonoBehaviour
	{
		private void OnCollisionEnter(Collision other)
		{
			KillPlayer(other.gameObject.GetComponentInParent<BaseAvatar>());
		}

		private void OnTriggerEnter(Collider other)
		{
			KillPlayer(other.gameObject.GetComponentInParent<BaseAvatar>());
		}

		private void KillPlayer(BaseAvatar avatar)
		{
			if (avatar == null) return;
			avatar.Kill();
			Debug.Log($"Killed {avatar.gameObject.name}");
		}
	}
}