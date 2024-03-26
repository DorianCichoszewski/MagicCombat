using UnityEngine;

namespace MagicCombat.Gameplay.Avatar.Movement
{
	public class DefaultMovement : IAvatarMovement
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