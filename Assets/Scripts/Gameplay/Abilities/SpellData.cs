using MagicCombat.Gameplay.Abilities.Base;
using MagicCombat.Gameplay.Spell.Interface;
using UnityEngine;

namespace MagicCombat.Gameplay.Abilities
{
	public class SpellData : ISpellData
	{
		public Vector2 direction;
		public Vector3 position;
		public AbilityCaster caster;
		public AbilitiesContext AbilitiesContext;

		public Vector2 Direction => direction;
		public Vector3 Position => position;

		public ISpellData Copy()
		{
			return new SpellData
			{
				direction = direction,
				position = position,
				caster = caster,
				AbilitiesContext = AbilitiesContext
			};
		}
	}
}