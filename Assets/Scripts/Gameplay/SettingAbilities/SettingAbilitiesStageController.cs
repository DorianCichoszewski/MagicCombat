using MagicCombat.Gameplay.Abilities;
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
			var abilitiesContext = ScriptableLocator.Get<AbilitiesContext>();
			var playerProvider = ScriptableLocator.Get<PlayerProvider>();
			foreach (var playerId in playerProvider.PlayersEnumerator)
			{
				abilitiesContext.AbilitiesData.Create(playerId, new AbilityPlayerData(abilitiesContext.InitialAbilities));
			}
		}
	}
}