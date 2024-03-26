using System.Collections.Generic;
using MagicCombat.Gameplay.Spell.Interface;
using MagicCombat.Gameplay.Spell.Property;
using UnityEngine;

namespace MagicCombat.Gameplay.Spell.LogicalFragment
{
	internal class ScaleSpellFragment : SpellLogicalFragment
	{
		[SerializeField]
		private SpellBuilderValue scale;

		public override List<PropertyId> RequiredProperties
		{
			get
			{
				var properties = new List<PropertyId>();
				if (scale.useProperty)
					properties.Add(scale.property);
				return properties;
			}
		}

		public override void Init(SpellObject spell)
		{
			float scaleFloat = scale.Evaluate(spell.Properties);
			spell.transform.localScale = new Vector3(scaleFloat, scaleFloat, scaleFloat);
		}

		public override void Tick(SpellObject spell, float deltaTime) { }

		public override void OnDestroyEvent(SpellObject spell) { }
	}
}