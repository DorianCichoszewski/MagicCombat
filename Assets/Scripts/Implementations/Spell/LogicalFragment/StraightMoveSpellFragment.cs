using System.Collections.Generic;
using MagicCombat.Gameplay.Spell.Interface;
using MagicCombat.Gameplay.Spell.Property;

namespace MagicCombat.Gameplay.Spell.LogicalFragment
{
	internal class StraightMoveSpellFragment : SpellLogicalFragment
	{
		public override List<PropertyId> RequiredProperties => new() { PropertyId.Speed };
		public override void Init(SpellObject spell) { }

		public override void Tick(SpellObject spell, float deltaTime)
		{
			var transform = spell.transform;
			transform.position += spell.Properties[PropertyId.Speed] * deltaTime * transform.forward;
		}

		public override void OnDestroyEvent(SpellObject spell) { }
	}
}