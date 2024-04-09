using MagicCombat.Shared.GameState;

namespace MagicCombat.Directors
{
	public interface IDirector
	{
		public void Run(BaseManager manager, SharedScriptable sharedScriptable);

		public void OnExit();
	}
}