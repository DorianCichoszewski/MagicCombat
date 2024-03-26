using System.Collections.Generic;
using MagicCombat.Gameplay.Abilities;
using MagicCombat.Gameplay.Avatar;
using MagicCombat.Gameplay.Spell.Interface;
using MagicCombat.Gameplay.Spell.Property;
using MagicCombat.Shared.Interfaces;

namespace MagicCombat.Gameplay.Spell.Event
{
	internal class AbilityFinishEvent : ISpellEventPlayerHit, ISpellEventGeneric
	{
		public List<PropertyId> RequiredProperties => null;
		public void Perform(SpellObject spell)
		{
			((SpellData)spell.Data).caster.State.onFinished?.Invoke();
		}

		public void Perform(SpellObject spell, ISpellTarget target)
		{
			((SpellData)spell.Data).caster.State.onFinished?.Invoke();
		}
	}
}