using System;
using MagicCombat.Gameplay.Abilities;
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
		private AbilitiesContext abilitiesContext;

		public override void Run()
		{
			base.Run();
			
			ScriptableLocator.Get<StagesManager>().NextStage();
		}

		public override void Enter()
		{
			LoadScriptable(gameplayRuntimeData);
			LoadScriptable(abilitiesContext);
		}
	}
}