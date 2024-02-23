namespace Gameplay.Limiters
{
	public interface ILimiter
	{
		public bool CanPerform();

		public void Start();

		public void Reset();

		public ILimiter Copy();
	}
}