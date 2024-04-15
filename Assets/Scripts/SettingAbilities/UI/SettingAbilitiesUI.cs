using System.Collections.Generic;
using UnityEngine;

namespace MagicCombat.SettingAbilities.UI
{
	public class SettingAbilitiesUI : MonoBehaviour
	{
		[SerializeField]
		private AbilitiesPlayerWindow windowPrefab;

		[SerializeField]
		private Transform windowsParent;

		[SerializeField]
		private SettingAbilitiesManager manager;

		private readonly List<AbilitiesPlayerWindow> spawnedWindows = new();

		public void Start()
		{
			var playerProvider = manager.SharedScriptable.PlayerProvider;
			foreach (var id in playerProvider.PlayersEnumerator)
			{
				var window = Instantiate(windowPrefab, windowsParent);
				window.Init(playerProvider, manager.GameModeData, id, this);
				spawnedWindows.Add(window);
			}
		}

		public void OnPlayerReady()
		{
			bool allReady = true;
			foreach (var window in spawnedWindows)
			{
				allReady &= window.IsReady;
			}

			if (allReady)
				manager.Next();
		}
	}
}