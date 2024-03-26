using System.Collections.Generic;
using MagicCombat.Gameplay.Spell.Property;
using UnityEngine;

namespace MagicCombat.Gameplay.Spell.Interface
{
	public abstract class SpellVisualFragment : MonoBehaviour, ISpellFragment
	{
		public abstract List<PropertyId> RequiredProperties { get; }

		public abstract void Init(SpellObject spell);

		public abstract void Tick(SpellObject spell, float deltaTime);
		public abstract void OnDestroyEvent(SpellObject spell);
	}
}