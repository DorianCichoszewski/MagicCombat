using System.Collections.Generic;
using Shared.Data;
using Shared.Interfaces;

namespace Shared.GameState
{
	public abstract class PlayerProvider : StartupScriptable
	{
		public abstract int PlayersCount { get; }

		public abstract void ClearCallbacks();

		public abstract void AddBot();

		public abstract IEnumerable<PlayerId> PlayersEnumerator { get; }

		public abstract PlayerId GetRandomPlayer();

		public abstract StaticPlayerData StaticData(PlayerId id);

		public abstract IPlayerInputController InputController(PlayerId id);

		public abstract IGameplayInputController GameplayInputController(PlayerId id);
	}
}