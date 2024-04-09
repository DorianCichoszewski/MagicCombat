using MagicCombat.Player;
using MagicCombat.SettingPlayer;
using MagicCombat.Shared.GameState;

namespace MagicCombat.Directors
{
	public class SettingPlayerDirector : IDirector
	{
		private PlayersManager playersManager;
		private SettingPlayerManager settingPlayerManager;

		public void Run(BaseManager manager, SharedScriptable sharedScriptable)
		{
			settingPlayerManager = (SettingPlayerManager)manager;

			playersManager = sharedScriptable.Essentials.gameObject.GetComponentInChildren<PlayersManager>();

			playersManager.onPlayerJoined += OnAddedPlayer;
			playersManager.onPlayerLeft += OnAddedPlayer;
		}

		private void OnAddedPlayer(PlayerInputController player)
		{
			settingPlayerManager.RefreshPlayers();
		}

		public void OnExit() { }
	}
}