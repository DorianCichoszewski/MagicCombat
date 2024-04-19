using MagicCombat.Shared.GameState;
using MagicCombat.Shared.StageFlow;

namespace MagicCombat.Gameplay
{
	public class GameplayStageController : IStageController
	{
		public void Run(SharedScriptable sharedScriptable) { }

		public void Return(SharedScriptable sharedScriptable)
		{
			// ToDo: unpause
		}

		public void Skip(SharedScriptable sharedScriptable)
		{
			var gameplayRuntimeData = (GameplayRuntimeData)sharedScriptable.ModeData;
			gameplayRuntimeData.GameMode.SimulateGame();
		}

		public void Exit(SharedScriptable sharedScriptable) { }
	}
}