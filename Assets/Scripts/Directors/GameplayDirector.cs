using System.Linq;
using MagicCombat.Gameplay;
using MagicCombat.Gameplay.Player;
using MagicCombat.Player;
using MagicCombat.Shared.GameState;

namespace MagicCombat.Directors
{
	public class GameplayDirector : IDirector
	{
		private PlayersManager playersManager;
		private GlobalState globalState;

		public void Run(BaseManager manager, GlobalState globalState)
		{
			this.globalState = globalState;
			var gameplayManager = (GameplayManager)manager;

			playersManager = globalState.gameObject.GetComponentInChildren<PlayersManager>();

			foreach (var inputController in playersManager.Players)
			{
				var data = inputController.Data;

				gameplayManager.CreatePlayer(data.gameplay, data.staticData, data.gameplayInput, data.id);
			}

			gameplayManager.OnGameEnd += OnGameEnd;
			gameplayManager.StartGame();
		}

		public void OnExit() { }

		private void OnGameEnd(PlayerAvatar winner)
		{
			playersManager.Players.First(player => player.Data.id == winner.Id).Data.points++;
			globalState.SharedScriptable.ProjectScenes.GoToSettingAbilities();
		}
	}
}