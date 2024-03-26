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
			var datas = manager.playersData;
			// Get correct number of windows
			while (playerConfigWindows.Count < datas.Count)
				playerConfigWindows.Add(Instantiate(windowPrefab, windowParent));
			while (playerConfigWindows.Count > datas.Count)
			{
				var window = playerConfigWindows[0];
				playerConfigWindows.RemoveAt(0);
				Destroy(window.gameObject);
			}

			// Set player
			for (int i = 0; i < playerConfigWindows.Count; i++)
			{
				var window = playerConfigWindows[i];
				window.SetPlayer(datas[i], this);
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