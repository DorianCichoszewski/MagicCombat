using System;
using Shared.GameState;
using Shared.StageFlow;
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
			Skip(sharedScriptable);

			sharedScriptable.StagesManager.NextStage();
		}

		public void Return(SharedScriptable sharedScriptable) { }

		public void Skip(SharedScriptable sharedScriptable)
		{
			gameplayRuntimeData.Init();
			sharedScriptable.ModeData = gameplayRuntimeData;
		}

		public void Exit(SharedScriptable sharedScriptable)
		{
			sharedScriptable.ModeData = null;
			gameplayRuntimeData.Disable();
		}
	}
}