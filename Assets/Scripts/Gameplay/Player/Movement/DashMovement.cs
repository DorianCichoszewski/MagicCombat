using Gameplay.Time;
using UnityEngine;

namespace Gameplay.Player.Basic
{
	public class DashMovement : IMovement
	{
		private Vector2 dashDirection;
		private float durationLeft;
		private MovementController movementController;
		private Timer timer;

		private readonly float speedMultiplier;
		private readonly float duration;

		public DashMovement(float speedMultiplier, float duration)
		{
			this.speedMultiplier = speedMultiplier;
			this.duration = duration;
		}

		public void Init(MovementController controller)
		{
			movementController = controller;

			dashDirection = movementController.lastMoveDirection;
			
			timer = new Timer(duration, EndDash, movementController.GameplayGlobals.clockManager);
		}

		public void Update(float deltaTime)
		{
			float speed = movementController.speed * speedMultiplier;

			movementController.ApplyMovement(speed * deltaTime * dashDirection);
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