using System.Collections.Generic;
using MagicCombat.SettingAbilities;
using UnityEngine;

namespace MagicCombat.UI.SettingAbilities
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
			var playersData = manager.RuntimeScriptable.playersData;
			foreach (var data in playersData)
			{
				var window = Instantiate(windowPrefab, windowsParent);
				window.Init(data.controller, this, manager.RuntimeScriptable);
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