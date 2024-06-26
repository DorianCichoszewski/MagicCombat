using MagicCombat.Gameplay.Abilities;
using MagicCombat.Gameplay.Spell;
using MagicCombat.Gameplay.Spell.Interface;
using MagicCombat.Gameplay.Spell.Property;
using UnityEngine;

namespace MagicCombat.Implementations.Spell.VisualFragment
{
	internal class ColorChangerSpellFragment : SpellVisualFragment
	{
		[SerializeField]
		private new MeshRenderer renderer;

		public override PropertyIdList RequiredProperties => null;

		public override void Init(SpellObject spell)
		{
			renderer.material = ((SpellData)spell.Data).caster.Avatar.InitData.material;
		}

		public override void Tick(SpellObject spell, float deltaTime) { }

		public override void OnDestroyEvent(SpellObject spell) { }
	}
}