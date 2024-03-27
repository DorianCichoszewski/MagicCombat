using MagicCombat.Gameplay.Spell;
using MagicCombat.Gameplay.Spell.Interface;
using MagicCombat.Gameplay.Spell.Property;
using UnityEngine;

namespace MagicCombat.Implementations.Spell.VisualFragment
{
	internal class AreaIndicatorSpellFragment : SpellVisualFragment
	{
		private const PropertyId Range = PropertyId.Range;
		public override PropertyIdList RequiredProperties => new(Range);

		public override void Init(SpellObject spell)
		{
			float range = spell.GetProperty(Range);
			transform.localScale = new Vector3(range, 1, range);
		}

		public override void Tick(SpellObject spell, float deltaTime) { }

		public override void OnDestroyEvent(SpellObject spell) { }
	}
}