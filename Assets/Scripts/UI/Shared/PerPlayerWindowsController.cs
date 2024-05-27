using System;
using System.Collections.Generic;
using Shared.Data;
using Shared.GameState;
using Shared.Notification;
using Shared.Services;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace MagicCombat.UI.Shared
{
	public class PerPlayerWindowsController : MonoBehaviour
	{
		[SerializeField]
		[Required]
		private EventChannelPlayer addedPlayerChannel;

		[SerializeField]
		[Required]
		private EventChannelPlayer removedPlayerChannel;

		[SerializeField]
		[Required]
		private PerPlayerWindow windowPrefab;

		[SerializeField]
		[Required]
		private Transform windowsParent;

		[SerializeField]
		[Required]
		private UnityEvent onReadyEvent;

		private readonly Dictionary<PlayerId, PerPlayerWindow> createdWindows = new();
		private Action onReady;

		private void Awake()
		{
			var manager = FindFirstObjectByType<BaseManager>();
			if (manager)
				CreateWindows(delegate { });

			addedPlayerChannel.OnRaised += UpdateWindow;
			removedPlayerChannel.OnRaised += UpdateWindow;
		}

		private void OnDestroy()
		{
			addedPlayerChannel.OnRaised -= UpdateWindow;
			removedPlayerChannel.OnRaised -= UpdateWindow;
		}

		public void CreateWindows(Action onAllWindowsReady)
		{
			onReady += onAllWindowsReady;
			
			var playerProvider = ScriptableLocator.Get<PlayerProvider>();

			foreach (var id in playerProvider.PlayersEnumerator)
			{
				var newWindow = Instantiate(windowPrefab, windowsParent);
				createdWindows.Add(id, newWindow);
				newWindow.Init(id, CheckWindowsReady);
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
					Destroy(playerWindow);
				}

				// Refresh window
				else
				{
					playerWindow.Init(changedPlayer, onReady);
				}
			}
			else
			{
				// Create new window for new / reconnected player
				var newWindow = Instantiate(windowPrefab, windowsParent);
				createdWindows.Add(changedPlayer, newWindow);
				newWindow.Init(changedPlayer, CheckWindowsReady);
			}
		}

		private void CheckWindowsReady()
		{
			foreach (var window in createdWindows.Values)
			{
				if (!window.IsReady)
					return;
			}

			onReadyEvent?.Invoke();
			onReady();
		}
	}
}