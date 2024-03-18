using System.Collections.Generic;
using System.Linq;

namespace MagicCombat.Spell.Event
{
	public class CreateSpellEvent : ISpellEventGeneric
	{
		public SpellPrototype prototype;

		public void Perform(Spell spell)
		{
			var newData = new SpellData
			{
				gameplayGlobals = spell.Data.gameplayGlobals,
				caster = spell.Data.caster,
				direction = spell.Data.direction,
				position = spell.Data.position
			};
			prototype.CreateNew(newData);
		}

		public List<PropertyId> RequiredProperties => prototype.properties.Select(x => x.Key).ToList();
	}
}