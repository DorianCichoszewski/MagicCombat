using System.Collections.Generic;
using UnityEngine;

namespace MagicCombat.Spell.LogicalFragment
{
	public class ScaleSpellFragment : SpellLogicalFragment
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

		public override void Init(Spell spell)
		{
			float scaleFloat = scale.Evaluate(spell.Properties);
			spell.transform.localScale = new Vector3(scaleFloat, scaleFloat, scaleFloat);
		}

		public override void Tick(Spell spell, float deltaTime) { }

		public override void OnDestroyEvent(Spell spell) { }
	}
}