namespace Gameplay.Player.Basic
{
	public interface IMovement
	{
		public void Init(MovementController controller);

		public void Update(float deltaTime);

		public void ChangeMovement();
	}
}