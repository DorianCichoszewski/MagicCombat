using MagicCombat.Gameplay.Player;
using UnityEngine;

namespace MagicCombat.Gameplay.Other
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
			player.Kill();
			Debug.Log($"Killed {player.PlayerController.name}");
		}
	}
}