using MagicCombat.Gameplay.Spell;
using MagicCombat.Gameplay.Spell.Interface;
using MagicCombat.Gameplay.Spell.Property;
using Shared.Interfaces;
using UnityEngine;
using UnityEngine.Scripting;

namespace MagicCombat.Implementations.Spell.Event
{
	[Preserve]
	internal class SelfDestroySpellEvent : ISpellEventGeneric, ISpellEventHit, ISpellEventPlayerHit
	{
		public PropertyIdList RequiredProperties => null;

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