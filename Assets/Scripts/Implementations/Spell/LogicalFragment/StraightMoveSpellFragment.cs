using MagicCombat.Gameplay.Spell;
using MagicCombat.Gameplay.Spell.Interface;
using MagicCombat.Gameplay.Spell.Property;
using UnityEngine.Scripting;

namespace MagicCombat.Implementations.Spell.LogicalFragment
{
	[Preserve]
	internal class StraightMoveSpellFragment : SpellLogicalFragment
	{
		private const PropertyId Speed = PropertyId.Speed;

		public override PropertyIdList RequiredProperties => new(Speed);
		public override void Init(SpellObject spell) { }

		public override void Tick(SpellObject spell, float deltaTime)
		{
			var transform = spell.transform;
			transform.position += spell.GetProperty(Speed) * deltaTime * transform.forward;
		}

		public override void OnDestroyEvent(SpellObject spell) { }
	}
}