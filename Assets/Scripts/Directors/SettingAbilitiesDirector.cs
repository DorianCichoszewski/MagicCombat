using System.Linq;
using MagicCombat.Player;
using MagicCombat.SettingAbilities;
using MagicCombat.Shared.GameState;

namespace MagicCombat.Directors
{
	public class SettingAbilitiesDirector : IDirector
	{
		public void Run(BaseManager manager, GlobalState globalState)
		{
			var settingAbilitiesManager = (SettingAbilitiesManager)manager;
			
			var playersManager = globalState.gameObject.GetComponentInChildren<PlayersManager>();
			
			settingAbilitiesManager.Init(playersManager.Players.Select(p => p.Data).ToList());
		}

		public void OnExit()
		{
		}
	}
}