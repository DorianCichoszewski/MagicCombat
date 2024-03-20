using System;
using Sirenix.OdinInspector;

namespace MagicCombat.Spell
{
	[InlineProperty]
	[Serializable]
	public struct SpellBuilderValue
	{
		private static ValueDropdownList<bool> valueList = new()
		{
			{ "Property", true },
			{ "Value", false }
		};

		[HorizontalGroup]
		[HideLabel]
		[ValueDropdown(nameof(valueList))]
		public bool useProperty;

		[HorizontalGroup]
		[HideLabel]
		[ShowIf(nameof(useProperty))]
		public PropertyId property;

		[HorizontalGroup]
		[HideLabel]
		[HideIf(nameof(useProperty))]
		public float value;

		public float Evaluate(PropertyGroup propertyGroup)
		{
			return useProperty ? propertyGroup[property] : value;
		}
	}
}