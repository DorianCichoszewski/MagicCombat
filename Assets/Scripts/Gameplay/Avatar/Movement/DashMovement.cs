using MagicCombat.Shared.Time;
using UnityEngine;

namespace MagicCombat.Gameplay.Avatar.Movement
{
	public class DashMovement : IAvatarMovement
	{
		private readonly float duration;

		private readonly float speedMultiplier;
		private Vector2 dashDirection;
		private float durationLeft;
		private MovementController movementController;
		private Timer timer;

		public DashMovement(float speedMultiplier, float duration)
		{
			this.speedMultiplier = speedMultiplier;
			this.duration = duration;
		}

		public void Init(MovementController controller)
		{
			movementController = controller;

			dashDirection = movementController.lastMoveDirection;

			timer = new Timer("Dash", duration, EndDash, movementController.ClockManager);
		}

		public Vector2 Update(float deltaTime)
		{
			float speed = movementController.speed * speedMultiplier;

			return speed * deltaTime * dashDirection;
		}

		public void ChangeMovement()
		{
			timer.Cancel();
		}

		private void EndDash()
		{
			movementController.ResetMovement();
		}
	}
}