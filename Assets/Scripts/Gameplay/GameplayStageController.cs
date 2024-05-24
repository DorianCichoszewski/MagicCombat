using Shared.GameState;
using Shared.StageFlow;
using UnityEngine;

namespace MagicCombat.Gameplay
{
	public class GameplayStageController : IStageController
	{
		public void Run(SharedScriptable sharedScriptable)
		{
			var manager = Object.FindAnyObjectByType<GameplayManager>();
			var gameplayData = (GameplayRuntimeData)sharedScriptable.ModeData;
			gameplayData.GameMode.Run(sharedScriptable, manager);
		}

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