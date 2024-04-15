using System;
using MagicCombat.Gameplay.Abilities;
using MagicCombat.Gameplay.Spell;
using MagicCombat.Gameplay.Spell.Interface;
using MagicCombat.Gameplay.Spell.Property;
using MagicCombat.Shared.Interfaces;
using UnityEngine;

namespace MagicCombat.Implementations.Spell.Event
{
	[Serializable]
	internal class AbilityChangeIconEvent : ISpellEventGeneric, ISpellEventPlayerHit
	{
		[SerializeField]
		private Sprite icon;

		public PropertyIdList RequiredProperties => null;

		public void Perform(SpellObject spell, ISpellTarget target)
		{
			((SpellData)spell.Data).caster.State.icon = icon;
		}

		public void Perform(SpellObject spell)
		{
			((SpellData)spell.Data).caster.State.icon = icon;
		}
	}
}