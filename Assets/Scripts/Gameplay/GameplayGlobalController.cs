using System;
using Shared.Services;
using Shared.StageFlow;
using UnityEngine;

namespace MagicCombat.Gameplay
{
	[Serializable]
	public class GameplayGlobalController : StageController
	{
		[SerializeField]
		private GameplayRuntimeData gameplayRuntimeData;
		
		[SerializeField]
		private BasicGameMode gameMode;

		public override void Run()
		{
			base.Run();
			
			ScriptableLocator.Get<StagesManager>().NextStage();
		}

		public override void Enter()
		{
			LoadScriptable(gameplayRuntimeData);
			LoadScriptable(gameMode);
		}
	}
}