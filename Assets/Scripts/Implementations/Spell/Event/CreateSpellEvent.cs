using MagicCombat.Gameplay.Abilities;
using MagicCombat.Gameplay.Spell;
using MagicCombat.Gameplay.Spell.Interface;
using MagicCombat.Gameplay.Spell.Property;
using UnityEngine.Scripting;

namespace MagicCombat.Implementations.Spell.Event
{
	[Preserve]
	internal class CreateSpellEvent : ISpellEventGeneric
	{
		public SpellPrototype prototype;

		public void Perform(SpellObject spell)
		{
			var spellData = (SpellData)spell.Data;
			var newData = spellData.Copy();
			spellData.AbilitiesContext.SpellCrafter.CreateNew(prototype, newData);
		}

		public PropertyIdList RequiredProperties => new(prototype.properties);
	}
}