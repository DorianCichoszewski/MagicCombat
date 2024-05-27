using Shared.GameState;
using Shared.Services;
using Shared.StageFlow;

namespace MagicCombat.Gameplay.SettingAbilities
{
	public class SettingAbilitiesStageController : StageController
	{
		public override void Run()
		{
			InitAbilities();
		}

		public override void Skip()
		{
			InitAbilities();
		}

		public override void Exit() { }

		private void InitAbilities()
		{
			var gameModeData = ScriptableLocator.Get<GameplayRuntimeData>();
			var playerProvider = ScriptableLocator.Get<PlayerProvider>();
			foreach (var playerId in playerProvider.PlayersEnumerator)
			{
				gameModeData.abilitiesData.Create(playerId, new AbilityPlayerData());
			}
		}
	}
}