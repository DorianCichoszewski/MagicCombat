using System.Collections.Generic;
using System.Linq;
using MagicCombat.Gameplay.Spell.Interface;
using MagicCombat.Gameplay.Spell.Property;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MagicCombat.Gameplay.Spell.VisualFragment
{
	internal class VisualFragmentsCombiner : SpellVisualFragment
	{
		[SerializeField]
		private List<SpellVisualFragment> visualFragments = new();

		public override List<PropertyId> RequiredProperties => visualFragments
			.SelectMany(fragment => fragment.RequiredProperties ?? Enumerable.Empty<PropertyId>()).ToList();

		public override void Init(SpellObject spell)
		{
			foreach (var fragment in visualFragments)
			{
				fragment.Init(spell);
			}
		}

		public override void Tick(SpellObject spell, float deltaTime)
		{
			foreach (var fragment in visualFragments)
			{
				fragment.Tick(spell, deltaTime);
			}
		}

		public override void OnDestroyEvent(SpellObject spell)
		{
			foreach (var fragment in visualFragments)
			{
				fragment.OnDestroyEvent(spell);
			}
		}

		[Button]
		private void GetVisualFragments()
		{
			visualFragments = GetComponentsInChildren<SpellVisualFragment>(true).ToList();
			visualFragments.Remove(this);
		}
	}
}