using System;

namespace MagicCombat.Shared.Data
{
	public class PlayerId : IComparable<PlayerId>
	{
		private readonly int internalId;

		// In range [0, playerCount)
		public int OrderedId { get; private set; }

		public bool IsControllerConnected { get; private set; }

		public static implicit operator int(PlayerId id)
		{
			return id.internalId;
		}

		public PlayerId(int id)
		{
			internalId = id;
		}

		public int CompareTo(PlayerId other)
		{
			if (ReferenceEquals(this, other)) return 0;
			if (ReferenceEquals(null, other)) return 1;
			return internalId.CompareTo(other.internalId);
		}

		public void ChangeOrderId(int newId)
		{
			OrderedId = newId;
		}

		public void ChangeControllerStatus(bool isConnected)
		{
			IsControllerConnected = isConnected;
		}
	}
}