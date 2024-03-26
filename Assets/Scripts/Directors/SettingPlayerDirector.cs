using System.Linq;
using MagicCombat.Player;
using MagicCombat.SettingPlayer;
using MagicCombat.Shared.GameState;

namespace MagicCombat.Directors
{
	public class SettingPlayerDirector : IDirector
	{
		private PlayersManager playersManager;
		private SettingPlayerManager settingPlayerManager;

		public void Run(BaseManager manager, GlobalState globalState)
		{
			settingPlayerManager = (SettingPlayerManager)manager;
			
			playersManager = globalState.gameObject.GetComponentInChildren<PlayersManager>();
			
			playersManager.onPlayerJoined += OnAddedPlayer;
			playersManager.onPlayerLeft += OnAddedPlayer;
		}

		private void OnAddedPlayer(PlayerInputController player)
		{
			settingPlayerManager.RefreshPlayers(playersManager.Players.Select(p => p.Data).ToList());
		}

		public void OnExit() { }
	}
}