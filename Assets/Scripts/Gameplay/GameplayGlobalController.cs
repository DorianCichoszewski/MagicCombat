using System;
using MagicCombat.Shared.GameState;
using MagicCombat.Shared.StageFlow;
using UnityEngine;

namespace MagicCombat.Gameplay
{
	[Serializable]
	public class GameplayGlobalController : IStageController
	{
		[SerializeField]
		private GameplayRuntimeData gameplayRuntimeData;

		public void Run(SharedScriptable sharedScriptable)
		{
			gameplayRuntimeData.Reset();
			sharedScriptable.ModeData = gameplayRuntimeData;
			sharedScriptable.StagesManager.NextStage();
		}

		public void Return(SharedScriptable sharedScriptable) { }

		public void Skip(SharedScriptable sharedScriptable) { }

		public void Exit(SharedScriptable sharedScriptable)
		{
			sharedScriptable.ModeData = null;
		}
	}
}