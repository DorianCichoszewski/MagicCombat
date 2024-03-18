namespace MagicCombat.Spell
{
	public interface ISpellFragment : ISpellPropertiesUser
	{
		public void Init(Spell spell);

		public void Tick(Spell spell, float deltaTime);

		public void OnDestroyEvent(Spell spell);
	}
}