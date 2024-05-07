using System;

namespace MagicCombat.Gameplay.Abilities
{
	[Flags]
	public enum AbilityType
	{
		None = 0,
		Basic = 1 << 0,
		Utility = 1 << 1,
	}
}