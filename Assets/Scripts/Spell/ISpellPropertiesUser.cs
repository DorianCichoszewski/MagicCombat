using System.Collections.Generic;

namespace MagicCombat.Spell
{
	public interface ISpellPropertiesUser
	{
		public List<PropertyId> RequiredProperties { get; }
	}
}