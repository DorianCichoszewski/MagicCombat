using System.Collections.Generic;
using UnityEngine;

namespace MagicCombat.Spell.VisualFragment
{
	public class AreaIndicatorSpellFragment : SpellVisualFragment
	{
		public override List<PropertyId> RequiredProperties => new() { PropertyId.Range };

		public override void Init(Spell spell)
		{
			float range = spell.Prototype.properties[PropertyId.Range];
			transform.localScale = new Vector3(range, 1, range);
		}

		public override void Tick(Spell spell, float deltaTime) { }

		public override void OnDestroyEvent(Spell spell) { }
	}
}