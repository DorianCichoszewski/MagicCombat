using System.Collections.Generic;
using MagicCombat.UI.Shared;
using UnityEngine;

namespace MagicCombat.SettingAbilities.UI
{
	public class SettingAbilitiesUI : MonoBehaviour
	{
		[SerializeField]
		private PerPlayerWindowsController windowsController;

		[SerializeField]
		private SettingAbilitiesManager manager;

		public void Start()
		{
			var playerProvider = manager.SharedScriptable.PlayerProvider;
			windowsController.CreateWindows(manager.SharedScriptable, manager.Next);
		}
	}
}