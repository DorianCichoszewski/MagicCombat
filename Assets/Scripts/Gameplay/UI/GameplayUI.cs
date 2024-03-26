using System.Collections.Generic;
using UnityEngine;

namespace MagicCombat.Gameplay.UI
{
	public class GameplayUI : MonoBehaviour
	{
		[SerializeField]
		private List<PlayerUI> playersUI = new();

		[SerializeField]
		private GameplayManager gameplayManager;

		private void Start()
		{
			gameObject.SetActive(false);
			gameplayManager.OnGameStarted += RefreshUI;
			RefreshUI();
		}

		private void RefreshUI()
		{
			foreach (var player in gameplayManager.AlivePlayers)
			{
				var playerUI = playersUI[player.Id];
				playerUI.Init(player);
			}

			gameObject.SetActive(true);
		}
	}
}