using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MagicCombat.Spell.VisualFragment
{
	public class VisualFragmentsCombiner : SpellVisualFragment
	{
		[SerializeField]
		private List<SpellVisualFragment> visualFragments = new();

		public override List<PropertyId> RequiredProperties => visualFragments
			.SelectMany(fragment => fragment.RequiredProperties ?? Enumerable.Empty<PropertyId>()).ToList();

		public override void Init(Spell spell)
		{
			foreach (var fragment in visualFragments)
			{
				fragment.Init(spell);
			}
		}

		public override void Tick(Spell spell, float deltaTime)
		{
			foreach (var fragment in visualFragments)
			{
				fragment.Tick(spell, deltaTime);
			}
		}

		public override void OnDestroyEvent(Spell spell)
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