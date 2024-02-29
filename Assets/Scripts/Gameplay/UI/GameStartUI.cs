using System.Collections.Generic;
using Gameplay.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI
{
	public class GameStartUI : MonoBehaviour
	{
		[SerializeField]
		private List<TMP_Text> playerTexts = new ();

		[SerializeField]
		private Button playButton;

		[SerializeField]
		private GameplayManager manager;

		private void Awake()
		{
			playButton.onClick.AddListener(manager.StartGame);
			manager.AddedPlayer += SetPlayer;
			manager.GameStarted += GameStarted;
			playButton.gameObject.SetActive(false);
		}

		private void SetPlayer(PlayerController player)
		{
			playerTexts[player.Index].text = $"{player.Input.devices[0].displayName}\n\nREADY";

			if (player.Index == playerTexts.Count - 1)
			{
				playButton.gameObject.SetActive(true);
				playButton.Select();
			}
		}

		private void GameStarted()
		{
			gameObject.SetActive(false);
		}
	}
}