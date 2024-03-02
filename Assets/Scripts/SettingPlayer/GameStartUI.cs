using System.Collections.Generic;
using Player;
using UnityEngine;

namespace SettingPlayer
{
	public class GameStartUI : MonoBehaviour
	{
		[SerializeField]
		private PlayerConfigWindow windowPrefab;
		[SerializeField]
		private Transform windowParent;

		[Space]
		[SerializeField]
		private SettingPlayerManager manager;
		
		private List<PlayerConfigWindow> playerConfigWindows = new ();

		private void Start()
		{
			manager.RuntimeScriptable.Essentials.playersManager.onPlayerJoined += SetPlayer;
			manager.RuntimeScriptable.Essentials.playersManager.onPlayerLeft += SetPlayer;
		}

		private void SetPlayer(PlayerController player)
		{
			var datas = manager.RuntimeScriptable.playersData;
			
			// Get correct number of windows
			while (playerConfigWindows.Count < datas.Count)
			{
				playerConfigWindows.Add(Instantiate(windowPrefab, windowParent));
			}
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
				window.SetPlayer(datas[i].playerController, this);
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