using MagicCombat.Gameplay.Spell;
using MagicCombat.Gameplay.Spell.Interface;
using MagicCombat.Gameplay.Spell.Property;
using UnityEngine;
using UnityEngine.Scripting;

namespace MagicCombat.Implementations.Spell.LogicalFragment
{
	[Preserve]
	internal class ScaleSpellFragment : SpellLogicalFragment
	{
		private const PropertyId Size = PropertyId.Size;

		public override PropertyIdList RequiredProperties => new(Size);

		public override void Init(SpellObject spell)
		{
			float scaleFloat = spell.GetProperty(Size);
			spell.transform.localScale = new Vector3(scaleFloat, scaleFloat, scaleFloat);
		}

		public override void Tick(SpellObject spell, float deltaTime) { }

		public override void OnDestroyEvent(SpellObject spell) { }
	}
}