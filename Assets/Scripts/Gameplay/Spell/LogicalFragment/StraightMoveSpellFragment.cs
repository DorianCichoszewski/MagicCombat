using System.Collections.Generic;

namespace MagicCombat.Spell.LogicalFragment
{
	public class StraightMoveSpellFragment : SpellLogicalFragment
	{
		public override List<PropertyId> RequiredProperties => new() { PropertyId.Speed };
		public override void Init(Spell spell) { }

		public override void Tick(Spell spell, float deltaTime)
		{
			var transform = spell.transform;
			transform.position += spell.Properties[PropertyId.Speed] * deltaTime * transform.forward;
		}

		public override void OnDestroyEvent(Spell spell) { }
	}
}