using MagicCombat.Gameplay;
using MagicCombat.Gameplay.Player;
using MagicCombat.Shared.GameState;

namespace MagicCombat.Directors
{
	public class GameplayDirector : IDirector
	{
		private SharedScriptable sharedScriptable;

		private GameplayManager gameplayManager;

		public void Run(BaseManager manager, SharedScriptable sharedScriptable)
		{
			this.sharedScriptable = sharedScriptable;
			gameplayManager = (GameplayManager)manager;

			var playerProvider = sharedScriptable.PlayerProvider;

			foreach (int index in playerProvider.PlayersIdEnumerator)
			{
				gameplayManager.CreatePlayer(playerProvider.StaticData(index),
					playerProvider.GameplayInputController(index), index);
			}

			gameplayManager.OnGameEnd += OnGameEnd;
			gameplayManager.StartGame();
		}

		public void OnExit() { }

		private void OnGameEnd(PlayerAvatar winner)
		{
			gameplayManager.GameModeData.points[winner.Id]++;
			sharedScriptable.ProjectScenes.GoToSettingAbilities();
		}
	}
}