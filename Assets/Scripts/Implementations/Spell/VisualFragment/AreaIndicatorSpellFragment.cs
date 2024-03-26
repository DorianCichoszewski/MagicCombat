using System.Collections.Generic;
using MagicCombat.Gameplay.Spell.Interface;
using MagicCombat.Gameplay.Spell.Property;
using UnityEngine;

namespace MagicCombat.Gameplay.Spell.VisualFragment
{
	internal class AreaIndicatorSpellFragment : SpellVisualFragment
	{
		public override List<PropertyId> RequiredProperties => new() { PropertyId.Range };

		public override void Init(SpellObject spell)
		{
			float range = spell.Prototype.properties[PropertyId.Range];
			transform.localScale = new Vector3(range, 1, range);
		}

		public override void Tick(SpellObject spell, float deltaTime) { }

		public override void OnDestroyEvent(SpellObject spell) { }
	}
}