namespace Gameplay.Limiters
{
	public interface ILimiter
	{
		public bool CanPerform();

		public bool TryPerform();

		public void Reset();

		public ILimiter Copy();
	}
}