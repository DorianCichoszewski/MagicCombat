using MagicCombat.Gameplay;
using Shared.GameState;
using Shared.StageFlow;
using UnityEngine;

namespace MagicCombat.SettingAbilities
{
	public class SettingAbilitiesStageController : IStageController
	{
		public void Run(SharedScriptable sharedScriptable)
		{
			InitAbilities(sharedScriptable);
		}

		public void Return(SharedScriptable sharedScriptable) { }

		public void Skip(SharedScriptable sharedScriptable)
		{
			InitAbilities(sharedScriptable);
		}

		public void Exit(SharedScriptable sharedScriptable) { }

		private void InitAbilities(SharedScriptable sharedScriptable)
		{
			var gameModeData = (GameplayRuntimeData)sharedScriptable.ModeData;
			var abilitiesContext = gameModeData.AbilitiesContext;
			foreach (var playerId in sharedScriptable.PlayerProvider.PlayersEnumerator)
			{
				gameModeData.abilitiesData.Create(playerId, new AbilityPlayerData(abilitiesContext.StartAbilitiesData));
			}
		}
	}
}