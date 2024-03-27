using System;
using MagicCombat.Gameplay.Spell.Property;
using Sirenix.OdinInspector;

namespace MagicCombat.Gameplay.Spell.Interface
{
	[Serializable]
	public abstract class SpellLogicalFragment : ISpellFragment
	{
		[ShowInInspector]
		[HideLabel]
		[ListDrawerSettings(DefaultExpandedState = true)]
		public abstract PropertyIdList RequiredProperties { get; }

		public abstract void Init(SpellObject spell);

		public abstract void Tick(SpellObject spell, float deltaTime);
		public abstract void OnDestroyEvent(SpellObject spell);
	}
}