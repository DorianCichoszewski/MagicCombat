using UnityEngine;

namespace Gameplay.Player.Basic
{
	public class DefaultMovement : IMovement
	{
		private MovementController movementController;

		public void Init(MovementController controller)
		{
			movementController = controller;
		}

		public Vector2 Update(float deltaTime)
		{
			return movementController.moveValue * (movementController.speed * deltaTime);
		}

		public void ChangeMovement() { }
	}
}