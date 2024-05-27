using Shared.GameState;
using Shared.Services;
using Shared.StageFlow;

namespace MagicCombat.Gameplay.SettingAbilities
{
	public class SettingAbilitiesManager : BaseManager
	{
		public void Next()
		{
			ScriptableLocator.Get<StagesManager>().NextStage();
		}
	}
}