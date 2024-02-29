using Gameplay.Player;
using UnityEngine;

namespace Gameplay.Other
{
	public class KillZone : MonoBehaviour
	{
		private void OnCollisionEnter(Collision other)
		{
			KillPlayer(other.gameObject.GetComponentInParent<PlayerAvatar>());
		}

		private void OnTriggerEnter(Collider other)
		{
			KillPlayer(other.gameObject.GetComponentInParent<PlayerAvatar>());
		}

		private void KillPlayer(PlayerAvatar player)
		{
			if (player == null) return;
			player.Hit();
			Debug.Log("Hit");
		}
	}
}