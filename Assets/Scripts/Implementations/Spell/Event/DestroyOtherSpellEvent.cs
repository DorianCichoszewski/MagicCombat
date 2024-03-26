using System.Collections.Generic;
using MagicCombat.Gameplay.Avatar;
using MagicCombat.Gameplay.Spell.Interface;
using MagicCombat.Gameplay.Spell.Property;
using MagicCombat.Shared.Interfaces;
using UnityEngine;

namespace MagicCombat.Gameplay.Spell.Event
{
	internal class DestroyOtherSpellEvent : ISpellEventPlayerHit, ISpellEventHit
	{
		public void Perform(SpellObject spell, GameObject other)
		{
			Object.Destroy(other);
		}

		public void Perform(SpellObject spell, ISpellTarget target)
		{
			target.Kill();
		}

		public List<PropertyId> RequiredProperties => new();
	}
}