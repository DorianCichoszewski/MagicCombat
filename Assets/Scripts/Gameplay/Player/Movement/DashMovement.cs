using MagicCombat.Gameplay.Time;
using UnityEngine;

namespace MagicCombat.Gameplay.Player.Movement
{
	public class DashMovement : IMovement
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

			timer = new Timer("Dash", duration, EndDash, movementController.GameplayGlobals.clockManager);
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