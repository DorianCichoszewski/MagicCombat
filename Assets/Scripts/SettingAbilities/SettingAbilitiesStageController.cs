using MagicCombat.Gameplay;
using MagicCombat.Shared.GameState;
using MagicCombat.Shared.StageFlow;
using UnityEngine;

namespace MagicCombat.SettingAbilities
{
	public class SettingAbilitiesStageController : IStageController
	{
		[SerializeField]
		private StartAbilitiesData startAbilitiesData;

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
			foreach (var playerId in sharedScriptable.PlayerProvider.PlayersEnumerator)
			{
				gameModeData.playerData.Create(playerId, new GameplayPlayerData(startAbilitiesData));
			}
		}
	}
}