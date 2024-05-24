using Shared.GameState;

namespace Shared.StageFlow
{
	public interface IStageController
	{
		// At entering stage
		public void Run(SharedScriptable sharedScriptable);

		// At closing child stage and returning focus
		public void Return(SharedScriptable sharedScriptable);

		// At closing stage
		public void Exit(SharedScriptable sharedScriptable);

		// At skipping stage
		public void Skip(SharedScriptable sharedScriptable);
	}
}