using MagicCombat.Gameplay;
using Shared.GameState;

namespace MagicCombat.SettingAbilities
{
	public class SettingAbilitiesManager : BaseManager
	{
		public GameplayRuntimeData GameModeData => (GameplayRuntimeData)sharedScriptable.ModeData;

		public void Next()
		{
			sharedScriptable.StagesManager.NextStage();
		}
	}
}