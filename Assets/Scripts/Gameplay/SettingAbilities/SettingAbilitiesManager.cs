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

		public void Next()
		{
			sharedScriptable.StagesManager.NextStage();
		}
	}
}