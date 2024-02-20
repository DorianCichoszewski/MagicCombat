using System;

namespace Gameplay.Limiters
{
	[Serializable]
	public class NoneLimiter : ILimiter
	{
		public bool CanPerform()
		{
			return true;
		}

		public bool TryPerform()
		{
			return true;
		}

		public void Reset() { }

		public ILimiter Copy()
		{
			return new NoneLimiter();
		}
	}
}