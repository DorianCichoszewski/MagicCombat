using System.Collections.Generic;
using UnityEngine;

namespace MagicCombat.Spell.Event
{
	public class SelfDestroySpellEvent : ISpellEventGeneric, ISpellEventHit, ISpellEventPlayerHit
	{
		public List<PropertyId> RequiredProperties => null;

		public void Perform(Spell spell)
		{
			spell.Destroy();
		}

		public void Perform(Spell spell, GameObject other)
		{
			spell.Destroy();
		}

		public void Perform(Spell spell, ISpellTarget target)
		{
			spell.Destroy();
		}
	}
}