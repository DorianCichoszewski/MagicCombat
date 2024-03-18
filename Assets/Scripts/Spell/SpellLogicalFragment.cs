using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace MagicCombat.Spell
{
	[Serializable]
	public abstract class SpellLogicalFragment : ISpellFragment
	{
		[ShowInInspector]
		[ListDrawerSettings(DefaultExpandedState = true)]
		public abstract List<PropertyId> RequiredProperties { get; }

		public abstract void Init(Spell spell);

		public abstract void Tick(Spell spell, float deltaTime);
		public abstract void OnDestroyEvent(Spell spell);
	}
}