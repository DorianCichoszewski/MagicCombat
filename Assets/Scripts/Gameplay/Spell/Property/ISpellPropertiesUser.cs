using System.Collections.Generic;

namespace MagicCombat.Gameplay.Spell.Property
{
	public interface ISpellPropertiesUser
	{
		public List<PropertyId> RequiredProperties { get; }
	}
}