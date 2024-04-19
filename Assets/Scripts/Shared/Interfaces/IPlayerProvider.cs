using System;
using System.Collections.Generic;
using MagicCombat.Shared.Data;

namespace MagicCombat.Shared.Interfaces
{
	public interface IPlayerProvider
	{
		public int PlayersCount { get; }

		public event Action<PlayerId> OnPlayerChanged;

		public void ClearCallbacks();

		public IEnumerable<PlayerId> PlayersEnumerator { get; }

		public PlayerId GetRandomPlayer();

		public StaticPlayerData StaticData(PlayerId id);

		public IPlayerInputController InputController(PlayerId id);

		public IGameplayInputController GameplayInputController(PlayerId id);
	}
}