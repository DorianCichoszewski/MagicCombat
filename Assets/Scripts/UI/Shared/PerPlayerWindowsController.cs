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
		private EventChannelUser addedUserChannel;

		[SerializeField]
		[Required]
		private EventChannelUser removedUserChannel;

		[SerializeField]
		[Required]
		private EventChannelUser statusChangedChannel;

		[SerializeField]
		[Required]
		private PerPlayerWindow windowPrefab;

		[SerializeField]
		[Required]
		private Transform windowsParent;

		[SerializeField]
		[Required]
		private UnityEvent onReadyEvent;

		private readonly Dictionary<UserId, PerPlayerWindow> createdWindows = new();
		private Action onReady;

		private void Awake()
		{
			var manager = FindFirstObjectByType<BaseManager>();
			if (manager)
				CreateWindows(delegate { });

			addedUserChannel.OnRaised += CreateWindow;
			statusChangedChannel.OnRaised += UpdateWindow;
			removedUserChannel.OnRaised += RemoveWindow;
		}

		private void OnDestroy()
		{
			addedUserChannel.OnRaised -= CreateWindow;
			statusChangedChannel.OnRaised -= UpdateWindow;
			removedUserChannel.OnRaised -= RemoveWindow;
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

		public void RemoveWindow(UserId changedUser)
		{
			if (createdWindows.Remove(changedUser, out var playerWindow)) Destroy(playerWindow);
		}

		private void CreateWindow(UserId user)
		{
			var newWindow = Instantiate(windowPrefab, windowsParent);
			createdWindows.Add(user, newWindow);
			newWindow.Init(user, CheckWindowsReady);
		}

		private void UpdateWindow(UserId user)
		{
			if (createdWindows.TryGetValue(user, out var playerWindow))
				playerWindow.Init(user, onReady);
			else
				Debug.LogError("Can't find window for user: " + user);
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