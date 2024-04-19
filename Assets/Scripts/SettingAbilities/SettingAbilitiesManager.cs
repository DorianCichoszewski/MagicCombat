using MagicCombat.Gameplay;
using MagicCombat.Shared.GameState;
using Sirenix.OdinInspector;

namespace MagicCombat.SettingAbilities
{
	public class SettingAbilitiesManager : BaseManager
	{
		[Required]
		public StartAbilitiesData startAbilities;

		public GameplayRuntimeData GameModeData => (GameplayRuntimeData)sharedScriptable.ModeData;

		protected override void OnAwake()
		{
			foreach (var playerId in sharedScriptable.PlayerProvider.PlayersEnumerator)
			{
				GameModeData.playerData.Create(playerId, new GameplayPlayerData(startAbilities));
			}
		}

		public void Next()
		{
			sharedScriptable.StagesManager.NextStage();
		}
	}
}