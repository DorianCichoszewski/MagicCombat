using System.Collections.Generic;
using System.Linq;

namespace MagicCombat.Spell.Event
{
	public class CreateSpellEvent : ISpellEventGeneric
	{
		public SpellPrototype prototype;

		public void Perform(Spell spell)
		{
			var spellData = (SpellData)spell.Data;
			var newData = spellData.Copy();
			spellData.gameplayGlobals.spellCrafter.CreateNew(prototype, newData);
		}

		public List<PropertyId> RequiredProperties => prototype.properties.Select(x => x.Key).ToList();
	}
}