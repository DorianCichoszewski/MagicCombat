using System;
using MagicCombat.Gameplay.Abilities;
using Shared.Services;
using Shared.StageFlow;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MagicCombat.Gameplay
{
	[Serializable]
	public class GameplayStageController : StageController
	{
		[SerializeField]
		private BasicGameMode basicGameMode;
		
		public override void Enter()
		{
			LoadScriptable(basicGameMode);
			ScriptableLocator.Get<AbilitiesContext>().ResetClocks();
			var manager = Object.FindAnyObjectByType<GameplayManager>();
			basicGameMode.Run(manager);
		}

		public override void Skip()
		{
			ScriptableLocator.Get<GameplayRuntimeData>().SimulateGame();
		}
	}
}