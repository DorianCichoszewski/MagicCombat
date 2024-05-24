using MagicCombat.Gameplay.Spell;
using MagicCombat.Gameplay.Spell.Interface;
using MagicCombat.Gameplay.Spell.Property;
using Shared.Interfaces;
using UnityEngine;
using UnityEngine.Scripting;

namespace MagicCombat.Implementations.Spell.Event
{
	[Preserve]
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

		public PropertyIdList RequiredProperties => new();
	}
}