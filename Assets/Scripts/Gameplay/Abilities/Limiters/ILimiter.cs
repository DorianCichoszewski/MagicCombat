using Sirenix.OdinInspector;

namespace MagicCombat.Gameplay.Abilities.Limiters
{
	[InlineProperty]
	[HideLabel]
	public interface ILimiter
	{
		public bool CanPerform();

		public void Start();

		public void Reset();

		public ILimiter Copy(AbilitiesContext abilitiesContext);
	}
}