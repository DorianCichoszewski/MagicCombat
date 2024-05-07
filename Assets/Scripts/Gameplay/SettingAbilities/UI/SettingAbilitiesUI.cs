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
			windowsController.CreateWindows(manager.SharedScriptable, manager.Next);
		}
	}
}