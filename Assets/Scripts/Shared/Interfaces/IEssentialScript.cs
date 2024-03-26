using MagicCombat.Shared.GameState;

namespace MagicCombat.Shared.Interfaces
{
	public interface IEssentialScript
	{
		public void Init(GlobalState globalState);

		public void Validate();
	}
}