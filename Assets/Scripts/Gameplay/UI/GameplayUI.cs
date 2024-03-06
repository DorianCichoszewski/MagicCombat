using System.Collections.Generic;
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
			gameplayManager.GameStarted += GameStarted;
		}

		private void GameStarted()
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