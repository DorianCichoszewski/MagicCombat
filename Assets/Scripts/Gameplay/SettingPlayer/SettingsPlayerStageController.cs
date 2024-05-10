using MagicCombat.Shared.GameState;
using MagicCombat.Shared.StageFlow;

namespace MagicCombat.SettingPlayer
{
	public class SettingsPlayerStageController : IStageController
	{
		public void Run(SharedScriptable sharedScriptable) { }

		public void Return(SharedScriptable sharedScriptable) { }

		public void Skip(SharedScriptable sharedScriptable)
		{
			for (int i = 0; i < 2; i++)
			{
				sharedScriptable.PlayerProvider.AddBot();
			}
		}

		public void Exit(SharedScriptable sharedScriptable) { }
	}
}