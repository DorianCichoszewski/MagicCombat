using System.Collections.Generic;
using UnityEngine;

namespace MagicCombat.Spell.VisualFragment
{
	public class ColorChangerSpellFragment : SpellVisualFragment
	{
		[SerializeField]
		private new MeshRenderer renderer;


		public override List<PropertyId> RequiredProperties => null;

		public override void Init(Spell spell)
		{
			renderer.material = spell.Data.caster.PlayerController.InitData.material;
		}

		public override void Tick(Spell spell, float deltaTime) { }

		public override void OnDestroyEvent(Spell spell) { }
	}
}