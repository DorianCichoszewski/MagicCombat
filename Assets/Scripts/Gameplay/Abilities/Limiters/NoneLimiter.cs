using System;

namespace MagicCombat.Gameplay.Abilities.Limiters
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

		public ILimiter Copy(AbilitiesContext abilitiesContext)
		{
			return new NoneLimiter();
		}
	}
}