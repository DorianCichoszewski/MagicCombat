using System.Collections.Generic;
using MagicCombat.Shared.Data;

namespace MagicCombat.Shared.Interfaces
{
	public interface IPlayerProvider
	{
		public int PlayersCount { get; }

		public IEnumerable<int> PlayersIdEnumerator { get; }
		
		public StaticPlayerData StaticData(int id);
		
		public IPlayerInputController InputController(int id);
		
		public IGameplayInputController GameplayInputController(int id);
	}
}