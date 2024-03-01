using System.Collections.Generic;
using UnityEngine;

namespace SettingAbilities
{
	public class SettingAbilitiesUI : MonoBehaviour
	{
		[SerializeField]
		private AbilitiesPlayerWindow windowPrefab;

		[SerializeField]
		private Transform windowsParent;

		[SerializeField]
		private SettingAbilitiesManager manager;

		private List<AbilitiesPlayerWindow> spawnedWindows = new();
		
		public void Init()
		{
			var playersData = manager.RuntimeScriptable.playersData;
			for (var i = 0; i < playersData.Count; i++)
			{
				var data = playersData[i];
				var window = Instantiate(windowPrefab, windowsParent);
				window.Init(data.playerController, manager.RuntimeScriptable);
				spawnedWindows.Add(window);
			}
		}
	}
}