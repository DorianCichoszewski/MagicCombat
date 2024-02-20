namespace Gameplay.Player.Basic
{
	public class DefaultMovement : IMovement
	{
		private MovementController movementController;

		public void Init(MovementController controller)
		{
			movementController = controller;
		}

		public void Update(float deltaTime)
		{
			movementController.ApplyMovement(movementController.moveValue * movementController.speed * deltaTime);
		}

		public void ChangeMovement() { }
	}
}