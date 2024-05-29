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

		public abstract IEnumerable<UserId> PlayersEnumerator { get; }

		public abstract UserId GetRandomUser();

		public abstract IUser GetUser(UserId id);
	}
}