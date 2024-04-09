using System;
using MagicCombat.Shared.Time;
using Sirenix.OdinInspector;

namespace MagicCombat.Gameplay.Abilities
{
	[Serializable]
	public class AbilitiesContext
	{
		public ClockManager clockManager;

		[Required]
		[AssetsOnly]
		public SpellCrafter spellCrafter;

		public void Reset()
		{
			clockManager.Reset();
		}
	}
}