using System;
using Shared.Services;
using Shared.StageFlow;
using Object = UnityEngine.Object;

namespace MagicCombat.Gameplay
{
	[Serializable]
	public class GameplayStageController : StageController
	{
		public override void Enter()
		{
			var manager = Object.FindAnyObjectByType<GameplayManager>();
			ScriptableLocator.Get<BasicGameMode>().Run(manager);
		}

		public override void Skip()
		{
			ScriptableLocator.Get<BasicGameMode>().SimulateGame();
		}
	}
}