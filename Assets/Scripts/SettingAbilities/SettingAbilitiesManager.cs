using MagicCombat.Gameplay;
using MagicCombat.Shared.GameState;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MagicCombat.SettingAbilities
{
	public class SettingAbilitiesManager : BaseManager
	{
		[Required]
		public StartAbilitiesData startAbilities;

		public GameplayRuntimeData GameModeData => (GameplayRuntimeData)sharedScriptable.GameModeData;

		public void Init()
		{
			foreach (int playerId in sharedScriptable.PlayerProvider.PlayersIdEnumerator)
			{
				GameModeData.playerData.Create(playerId, new GameplayPlayerData(startAbilities));
			}
		}

		public void Next()
		{
			Debug.Log("Finished abilities setup");
			sharedScriptable.ProjectScenes.GoToGameplay();
		}
	}
}