using System.Collections.Generic;
using Shared.Data;
using Shared.Notification;
using UnityEngine;

namespace MagicCombat.Gameplay.UI
{
	public class GameplayUI : MonoBehaviour
	{
		[SerializeField]
		private EventChannelPlayer playerCreatedChannel;
		
		[SerializeField]
		private EventChannelPlayer playerDeadChannel;

		
		[SerializeField]
		private List<PlayerUI> playersUI = new();

		[SerializeField]
		private GameplayManager gameplayManager;

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

		private void RefreshPlayer(PlayerId id)
		{
			var playerUI = playersUI[id.OrderedId];
			playerUI.Init(gameplayManager.Mode.GetPlayer(id));
			playerUI.gameObject.SetActive(true);
		}
		
		private void DisablePlayer(PlayerId id)
		{
			var playerUI = playersUI[id.OrderedId];
			playerUI.gameObject.SetActive(false);
		}
	}
}