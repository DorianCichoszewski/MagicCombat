using MagicCombat.Gameplay.Spell.Property;

namespace MagicCombat.Gameplay.Spell.Interface
{
	public interface ISpellFragment : ISpellPropertiesUser
	{
		public void Init(SpellObject spell);

		public void Tick(SpellObject spell, float deltaTime);

		public void OnDestroyEvent(SpellObject spell);
	}
}