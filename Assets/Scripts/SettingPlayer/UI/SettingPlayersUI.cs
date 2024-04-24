using MagicCombat.UI.Shared;
using UnityEngine;

namespace MagicCombat.SettingPlayer.UI
{
	public class SettingPlayersUI : MonoBehaviour
	{
		[SerializeField]
		private PerPlayerWindowsController windowsController;

		[Space]
		[SerializeField]
		private SettingPlayerManager manager;

		private void Start()
		{
			windowsController.CreateWindows(manager.SharedScriptable, manager.ConfirmPlayers);
			manager.OnRefreshPlayers += windowsController.UpdateWindow;
		}
	}
}