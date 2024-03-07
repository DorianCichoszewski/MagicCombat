using System.Collections.Generic;
using MagicCombat.Gameplay;
using UnityEngine;

namespace MagicCombat.UI.Gameplay
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
			gameplayManager.GameStarted += RefreshUI;
			gameplayManager.AvatarsChanged += RefreshUI;
			RefreshUI();
		}

		private void RefreshUI()
		{
			for (int i = 0; i < gameplayManager.RuntimeScriptable.playersData.Count; i++)
			{
				var playerUI = playersUI[i];
				var playerData = gameplayManager.RuntimeScriptable.playersData[i];
				playerUI.SetPlayer(playerData.controller);
				playerUI.Init();
			}

			gameObject.SetActive(true);
		}
	}
}