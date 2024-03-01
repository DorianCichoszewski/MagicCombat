using System.Collections.Generic;
using Extension;
using Player;
using UnityEngine;
using UnityEngine.UI;

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
		[SerializeField]
		private Button nextButton;
		
		private List<PlayerConfigWindow> playerConfigWindows = new ();

		private void Start()
		{
			nextButton.gameObject.SetActive(false);
			nextButton.onClick.AddListener(manager.ConfirmPlayers);
			SetCallbacks();
		}
		
		private void SetCallbacks()
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
				window.SetPlayer(datas[i].playerController);
			}

			if (datas.Count >= manager.MinPlayers && datas.Count <= manager.MaxPlayers)
			{
				nextButton.gameObject.SetActiveCached(true);
				nextButton.Select();
			}
			else
			{
				nextButton.gameObject.SetActiveCached(false);
			}
		}
	}
}