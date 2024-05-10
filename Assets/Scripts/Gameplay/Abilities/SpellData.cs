using MagicCombat.Gameplay.Abilities.Base;
using MagicCombat.Gameplay.Spell.Interface;
using MagicCombat.Shared.Interfaces;
using UnityEngine;

namespace MagicCombat.Gameplay.Abilities
{
	public class SpellData : ISpellData
	{
		public Vector2 direction;
		public Vector3 position;
		public AbilityCaster caster;
		public AbilitiesContext AbilitiesContext;

		public Vector2 Direction
		{
			get => direction;
			set => direction = value;
		}

		public Vector3 Position
		{
			get => position;
			set => position = value;
		}

		public ISpellTarget CasterSpellTarget => caster.Avatar;

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