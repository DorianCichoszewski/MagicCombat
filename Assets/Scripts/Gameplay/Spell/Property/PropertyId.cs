using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace MagicCombat.Gameplay.Spell.Property
{
	public enum PropertyId
	{
		Speed,
		Acceleration,
		Duration,
		Range,
		Force,
		Length,
		Count,
		Size
	}

	[DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.OneLine, IsReadOnly = true)]
	public sealed class PropertyGroup : Dictionary<PropertyId, float> { }
}