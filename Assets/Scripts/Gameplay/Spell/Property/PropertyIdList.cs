using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace MagicCombat.Gameplay.Spell.Property
{
	[Serializable]
	public class PropertyIdList : IEnumerable<PropertyId>
	{
		[ShowInInspector]
		private readonly List<PropertyId> list = new();

		public PropertyIdList()
		{
			list = new();
		}
		
		public PropertyIdList(PropertyId id)
		{
			list = new(1){id};
		}

		public PropertyIdList(PropertyGroup group)
		{
			if (group == null) return;
			
			foreach (var property in group.Keys)
			{
				list.Add(property);
			}
		}

		public PropertyIdList Add(PropertyId id)
		{
			if (!list.Contains(id))
			{
				list.Add(id);
			}

			return this;
		}

		public PropertyIdList Add(SpellBuilderValue value)
		{
			return value.useProperty ? Add(value.property) : this;
		}

		public PropertyIdList Add(PropertyIdList propertyList)
		{
			if (propertyList == null) return this;
			
			foreach (var id in propertyList.list)
			{
				Add(id);
			}

			return this;
		}

		public PropertyIdList Add(ISpellPropertiesUser value)
		{
			if (value == null) return this;
			
			return Add(value.RequiredProperties);
		}
		
		public PropertyIdList Add(IEnumerable<ISpellPropertiesUser> values)
		{
			foreach (var value in values)
			{
				Add(value.RequiredProperties);
			}

			return this;
		}
		
		public IEnumerator<PropertyId> GetEnumerator()
		{
			return list.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}