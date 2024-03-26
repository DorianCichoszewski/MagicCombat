using MagicCombat.Gameplay.Spell.Property;
using MagicCombat.Shared.Interfaces;
using UnityEngine;

namespace MagicCombat.Gameplay.Spell.Interface
{
	public interface ISpellEventGeneric : ISpellPropertiesUser
	{
		public void Perform(SpellObject spell);
	}

	public interface ISpellEventPlayerHit : ISpellPropertiesUser
	{
		public void Perform(SpellObject spell, ISpellTarget target);
	}

	public interface ISpellEventHit : ISpellPropertiesUser
	{
		public void Perform(SpellObject spell, GameObject other);
	}
}