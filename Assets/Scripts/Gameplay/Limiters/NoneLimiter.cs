using System;

namespace MagicCombat.Gameplay.Limiters
{
	[Serializable]
	public class NoneLimiter : ILimiter
	{
		public bool CanPerform()
		{
			return true;
		}

		public void Start() { }

		public void Reset() { }

		public ILimiter Copy(GameplayGlobals gameplayGlobals)
		{
			return new NoneLimiter();
		}
	}
}