using System.Collections.Generic;
using UnityEngine;

namespace MagicCombat.Spell
{
	public abstract class SpellVisualFragment : MonoBehaviour, ISpellFragment
	{
		public abstract List<PropertyId> RequiredProperties { get; }

		public abstract void Init(Spell spell);

		public abstract void Tick(Spell spell, float deltaTime);
		public abstract void OnDestroyEvent(Spell spell);
	}
}