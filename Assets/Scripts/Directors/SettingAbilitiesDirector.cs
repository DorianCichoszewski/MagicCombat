using MagicCombat.SettingAbilities;
using MagicCombat.Shared.GameState;

namespace MagicCombat.Directors
{
	public class SettingAbilitiesDirector : IDirector
	{
		public void Run(BaseManager manager, SharedScriptable sharedScriptable)
		{
			var settingAbilitiesManager = (SettingAbilitiesManager)manager;

			settingAbilitiesManager.Init();
		}

		public void OnExit() { }
	}
}