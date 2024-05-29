using System;
using Sirenix.OdinInspector;

namespace Shared.Data
{
	public class UserId : IComparable<UserId>
	{
		[ShowInInspector]
		private readonly int internalId;

		// In range [0, playerCount)
		public int OrderedId { get; private set; }

		public static implicit operator int(UserId id)
		{
			return id.internalId;
		}

		public UserId(int id)
		{
			internalId = id;
		}

		public int CompareTo(UserId other)
		{
			if (ReferenceEquals(this, other)) return 0;
			if (ReferenceEquals(null, other)) return 1;
			return internalId.CompareTo(other.internalId);
		}

		public void ChangeOrderId(int newId)
		{
			OrderedId = newId;
		}
	}
}