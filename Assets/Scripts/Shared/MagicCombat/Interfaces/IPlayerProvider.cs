using System.Collections.Generic;
using Shared.Data;

namespace Shared.Interfaces
{
	public interface IPlayerProvider
	{
		public int PlayersCount { get; }

		public void ClearCallbacks();

		public void AddBot();

		public IEnumerable<PlayerId> PlayersEnumerator { get; }

		public PlayerId GetRandomPlayer();

		public StaticPlayerData StaticData(PlayerId id);

		public IPlayerInputController InputController(PlayerId id);

		public IGameplayInputController GameplayInputController(PlayerId id);
	}
}