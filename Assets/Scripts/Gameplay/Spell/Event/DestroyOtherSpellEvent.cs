using System.Collections.Generic;
using UnityEngine;

namespace MagicCombat.Spell.Event
{
	public class DestroyOtherSpellEvent : ISpellEventPlayerHit, ISpellEventHit
	{
		public void Perform(Spell spell, GameObject other)
		{
			Object.Destroy(other);
		}

		public void Perform(Spell spell, ISpellTarget target)
		{
			target.Kill();
		}

		public List<PropertyId> RequiredProperties => new();
	}
}