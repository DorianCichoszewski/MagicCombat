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