using MagicCombat.Player;
using MagicCombat.SettingPlayer;
using MagicCombat.Shared.Data;
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

			sharedScriptable.PlayerProvider.OnPlayerChanged += OnAddedPlayer;
		}

		private void OnAddedPlayer(PlayerId player)
		{
			settingPlayerManager.RefreshPlayers();
		}

		public void OnExit() { }
	}
}