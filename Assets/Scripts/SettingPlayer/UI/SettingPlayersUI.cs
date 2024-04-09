using System.Collections.Generic;
using UnityEngine;

namespace MagicCombat.SettingPlayer.UI
{
	public class SettingPlayersUI : MonoBehaviour
	{
		[SerializeField]
		private PlayerConfigWindow windowPrefab;

		[SerializeField]
		private Transform windowParent;

		[Space]
		[SerializeField]
		private SettingPlayerManager manager;

		private readonly List<PlayerConfigWindow> playerConfigWindows = new();

		private void Start()
		{
			manager.OnRefreshPlayers += SetPlayers;
		}

		private void SetPlayers()
		{
			var playerProvider = manager.SharedScriptable.PlayerProvider;

			// Get correct number of windows
			while (playerConfigWindows.Count < playerProvider.PlayersCount)
				playerConfigWindows.Add(Instantiate(windowPrefab, windowParent));
			while (playerConfigWindows.Count > playerProvider.PlayersCount)
			{
				var window = playerConfigWindows[^1];
				playerConfigWindows.RemoveAt(playerConfigWindows.Count - 1);
				Destroy(window.gameObject);
			}

			// Set player
			int windowIndex = 0;
			foreach (int id in playerProvider.PlayersIdEnumerator)
			{
				var window = playerConfigWindows[windowIndex];
				window.SetPlayer(playerProvider, id, this);
				windowIndex++;
			}
		}

		public void OnPlayerReady()
		{
			bool allReady = true;
			foreach (var window in playerConfigWindows)
			{
				allReady &= window.IsReady;
			}

			if (allReady)
				manager.ConfirmPlayers();
		}
	}
}