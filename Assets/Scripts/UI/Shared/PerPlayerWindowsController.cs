using System;
using System.Collections.Generic;
using Shared.Data;
using Shared.GameState;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MagicCombat.UI.Shared
{
	[Serializable]
	[InlineProperty]
	[HideLabel]
	public class PerPlayerWindowsController
	{
		[SerializeField]
		[Required]
		private PerPlayerWindow windowPrefab;

		[SerializeField]
		[Required]
		private Transform windowsParent;

		private Dictionary<PlayerId, PerPlayerWindow> createdWindows = new ();
		private SharedScriptable sharedScriptable;
		private Action onReady;

		public void CreateWindows(SharedScriptable shared, Action onAllWindowsReady)
		{
			sharedScriptable = shared;
			onReady = onAllWindowsReady;

			foreach (var id in shared.PlayerProvider.PlayersEnumerator)
			{
				var newWindow = Object.Instantiate(windowPrefab, windowsParent);
				createdWindows.Add(id, newWindow);
				newWindow.Init(shared, id, CheckWindowsReady);
			}
		}

		public void UpdateWindow(PlayerId changedPlayer)
		{
			if (createdWindows.TryGetValue(changedPlayer, out var playerWindow))
			{
				// Destroy window if player is disconnected
				if (!changedPlayer.IsControllerConnected)
				{
					createdWindows.Remove(changedPlayer);
					Object.Destroy(playerWindow);
				}

				// Refresh window
				else
				{
					playerWindow.Init(sharedScriptable, changedPlayer, onReady);
				}
			}
			else
			{
				// Create new window for new / reconnected player
				var newWindow = Object.Instantiate(windowPrefab, windowsParent);
				createdWindows.Add(changedPlayer, newWindow);
				newWindow.Init(sharedScriptable, changedPlayer, CheckWindowsReady);
			}
		}

		private void CheckWindowsReady()
		{
			foreach (var window in createdWindows.Values)
			{
				if (!window.IsReady)
					return;
			}

			onReady();
		}
	}
}