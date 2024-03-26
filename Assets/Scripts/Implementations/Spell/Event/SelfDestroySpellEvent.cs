using System.Collections.Generic;
using MagicCombat.Gameplay.Avatar;
using MagicCombat.Gameplay.Spell.Interface;
using MagicCombat.Gameplay.Spell.Property;
using MagicCombat.Shared.Interfaces;
using UnityEngine;

namespace MagicCombat.Gameplay.Spell.Event
{
	internal class SelfDestroySpellEvent : ISpellEventGeneric, ISpellEventHit, ISpellEventPlayerHit
	{
		public List<PropertyId> RequiredProperties => null;

		public void Perform(SpellObject spell)
		{
			spell.Destroy();
		}

		public void Perform(SpellObject spell, GameObject other)
		{
			spell.Destroy();
		}

		public void Perform(SpellObject spell, ISpellTarget target)
		{
			spell.Destroy();
		}
	}
}