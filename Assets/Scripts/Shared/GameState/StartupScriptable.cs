using Shared.Services;

namespace Shared.GameState
{
	public abstract class StartupScriptable : ScriptableService
	{
		public abstract void GameStart();
	}
}