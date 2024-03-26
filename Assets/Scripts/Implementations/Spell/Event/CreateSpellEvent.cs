using System.Collections.Generic;
using System.Linq;
using MagicCombat.Gameplay.Abilities;
using MagicCombat.Gameplay.Spell.Interface;
using MagicCombat.Gameplay.Spell.Property;

namespace MagicCombat.Gameplay.Spell.Event
{
	internal class CreateSpellEvent : ISpellEventGeneric
	{
		public SpellPrototype prototype;

		public void Perform(SpellObject spell)
		{
			var spellData = (SpellData)spell.Data;
			var newData = spellData.Copy();
			spellData.AbilitiesContext.spellCrafter.CreateNew(prototype, newData);
		}

		public List<PropertyId> RequiredProperties => prototype.properties.Select(x => x.Key).ToList();
	}
}