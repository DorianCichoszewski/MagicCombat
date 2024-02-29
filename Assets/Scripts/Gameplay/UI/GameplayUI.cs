using System.Collections.Generic;
using Gameplay.Player;
using UnityEngine;

namespace Gameplay.UI
{
	public class GameplayUI : MonoBehaviour
	{
		[SerializeField]
		private List<PlayerUI> playersUI = new();

		[SerializeField]
		private GameplayManager gameplayManager;

		private void Awake()
		{
			gameObject.SetActive(false);
			gameplayManager.AddedPlayer += PlayerSetup;
			gameplayManager.GameStarted += GameStarted;
		}

		private void PlayerSetup(PlayerController player)
		{
			playersUI[player.Index].SetPlayer(player);
		}

		private void GameStarted()
		{
			foreach (var playerUI in playersUI)
			{
				playerUI.Init();
			}
			
			gameObject.SetActive(true);
		}
	}
}