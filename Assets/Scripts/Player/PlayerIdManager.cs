using System.Collections.Generic;
using MagicCombat.Shared.Data;
using MagicCombat.Shared.Interfaces;

namespace MagicCombat.Player
{
	public class PlayerIdManager
	{
		private readonly List<PlayerId> usedIds = new();
		private readonly Queue<PlayerId> unconnectedPlayersIds = new();

		private int nextId;

		public void AddPlayer(IPlayerInputController player)
		{
			var newId = GetId();
			player.SetId(newId);
			usedIds.Add(newId);

			SetOrder();
		}

		public void RemovePlayer(IPlayerInputController player)
		{
			var id = player.Id;
			id.ChangeControllerStatus(false);
			usedIds.Remove(id);
			unconnectedPlayersIds.Enqueue(id);

			SetOrder();
		}

		private PlayerId GetId()
		{
			if (unconnectedPlayersIds.Count > 0)
				return unconnectedPlayersIds.Dequeue();

			return new PlayerId(++nextId);
		}

		private void SetOrder()
		{
			for (int i = 0; i < usedIds.Count; i++)
			{
				usedIds[i].ChangeOrderId(i);
			}
		}
	}
}