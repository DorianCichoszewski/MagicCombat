using System.Collections.Generic;
using MagicCombat.Gameplay.Notifications;
using MagicCombat.Gameplay.Player;
using UnityEngine;

namespace MagicCombat.Gameplay.UI
{
	public class GameplayUI : MonoBehaviour
	{
		[SerializeField]
		private EventChannelPlayerAvatar playerCreatedChannel;

		[SerializeField]
		private EventChannelPlayerAvatar playerDeadChannel;


		[SerializeField]
		private List<PlayerUI> playersUI = new();

		private void Awake()
		{
			foreach (var playerUI in playersUI)
			{
				playerUI.gameObject.SetActive(false);
			}

			gameObject.SetActive(true);

			playerCreatedChannel.OnRaised += RefreshPlayer;
			playerDeadChannel.OnRaised += DisablePlayer;
		}

		private void OnDestroy()
		{
			playerCreatedChannel.OnRaised -= RefreshPlayer;
			playerDeadChannel.OnRaised -= DisablePlayer;
		}

		private void RefreshPlayer(PlayerAvatar player)
		{
			var playerUI = playersUI[player.Id.OrderedId];
			playerUI.Init(player);
			playerUI.gameObject.SetActive(true);
		}

		private void DisablePlayer(PlayerAvatar player)
		{
			var playerUI = playersUI[player.Id.OrderedId];
			playerUI.gameObject.SetActive(false);
		}
	}
}