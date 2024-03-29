using MagicCombat.Gameplay.Abilities;
using MagicCombat.Gameplay.Spell;
using MagicCombat.Gameplay.Spell.Interface;
using MagicCombat.Gameplay.Spell.Property;
using MagicCombat.Shared.Interfaces;
using UnityEngine.Scripting;

namespace MagicCombat.Implementations.Spell.Event
{
	[Preserve]
	internal class AbilityFinishEvent : ISpellEventPlayerHit, ISpellEventGeneric
	{
		public PropertyIdList RequiredProperties => null;
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