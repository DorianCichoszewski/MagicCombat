using System;

namespace MagicCombat.Gameplay.Abilities.Limiters
{
	[Serializable]
	public class ChargesLimiter : ILimiter
	{
		public float maxCharges = 3;
		public float duration = 3f;

		public bool CanPerform()
		{
			throw new NotImplementedException();
		}

		public void Start()
		{
			throw new NotImplementedException();
		}

		public void Reset()
		{
			throw new NotImplementedException();
		}

		public ILimiter Copy(AbilitiesContext abilitiesContext)
		{
			return new ChargesLimiter
			{
				maxCharges = maxCharges,
				duration = duration
			};
		}
	}
}