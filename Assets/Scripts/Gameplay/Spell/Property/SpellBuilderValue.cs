using System;
using Sirenix.OdinInspector;

namespace MagicCombat.Gameplay.Spell.Property
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

		public SpellBuilderValue(bool useProperty, PropertyId id = PropertyId.Speed)
		{
			this.useProperty = useProperty;
			property = id;
			value = 0;
		}

		public float Evaluate(PropertyGroup propertyGroup)
		{
			return useProperty ? propertyGroup[property] : value;
		}

		public float Evaluate(SpellObject spellObject)
		{
			return Evaluate(spellObject.Properties);
		}
	}
}