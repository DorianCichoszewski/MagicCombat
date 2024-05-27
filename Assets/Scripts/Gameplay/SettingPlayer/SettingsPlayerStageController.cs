using Shared.GameState;
using Shared.Services;
using Shared.StageFlow;

namespace MagicCombat.SettingPlayer
{
	public class SettingsPlayerStageController : StageController
	{
		public override void Run() { }

		public override void Skip()
		{
			var playerProvider = ScriptableLocator.Get<PlayerProvider>();
			for (int i = 0; i < 2; i++)
			{
				playerProvider.AddBot();
			}
		}

		public override void Exit() { }
	}
}