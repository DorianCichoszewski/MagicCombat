using Gameplay.Player.Movement;
using MagicCombat.Extension;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MagicCombat.Gameplay.Player.Movement
{
	public class MovementController : MonoBehaviour
	{
		public float speed;

		[Header("Collisions")]
		[SerializeField]
		private LayerMask collisionLayerMask;

		[SerializeField]
		private int collisionRayCount = 12;

		[SerializeField]
		private float characterColliderSize = 1;

		[Header("Debug")]
		public Vector2 lastMoveDirection;

		public Vector2 moveValue;

		private IMovement currentMovement;
		private MovementExternalController externalController;
		private Vector2 rotationInput;

		private float targetRotationAngle;
		private bool useStickRotation;

		public GameplayGlobals GameplayGlobals { get; private set; }

		public Vector2 LookDirection => rotationInput != Vector2.zero ? rotationInput : lastMoveDirection;

		public void Init(GameplayGlobals gameplayGlobals)
		{
			ResetMovement();
			externalController = new MovementExternalController();

			GameplayGlobals = gameplayGlobals;

			gameplayGlobals.clockManager.FixedClock.ClockUpdate += HandleMovement;
		}

		public void Move(InputAction.CallbackContext ctx)
		{
			moveValue = ctx.ReadValue<Vector2>();
			if (moveValue != Vector2.zero) lastMoveDirection = moveValue.normalized;
		}

		public void Rotate(InputAction.CallbackContext ctx)
		{
			rotationInput = ctx.ReadValue<Vector2>();
			if (rotationInput != Vector2.zero)
			{
				useStickRotation = true;
				targetRotationAngle = rotationInput.ToAngleRotation();
			}
			else
			{
				useStickRotation = false;
			}
		}

		private void ApplyMovement(Vector2 targetMovement)
		{
			// Split big movements to better check collision
			int movementSegmentCount = (int)(targetMovement.magnitude / (characterColliderSize * 0.5)) + 1;
			var movementSegment = targetMovement * (1.0f / movementSegmentCount);

			for (int i = 0; i < movementSegmentCount; i++)
			{
				transform.Translate2(movementSegment);
				AvoidObstacles();
			}
		}

		public void SetMovement<T>(T movement) where T : IMovement
		{
			currentMovement = movement;
			currentMovement.Init(this);
		}

		public void ResetMovement()
		{
			SetMovement(new DefaultMovement());
		}

		public void AddForce(MovementForce force)
		{
			externalController.AddForce(force);
		}

		private void HandleMovement(float deltaTime)
		{
			if (!enabled)
			{
				AvoidObstacles();
				return;
			}

			if (!useStickRotation)
				targetRotationAngle = lastMoveDirection.ToAngleRotation();

			var targetRotation = Quaternion.Euler(0f, targetRotationAngle, 0f);
			transform.rotation = targetRotation;

			var movement = currentMovement.Update(deltaTime);
			movement += externalController.Update(deltaTime);

			ApplyMovement(movement);
		}

		private void AvoidObstacles()
		{
			var offset = Vector3.zero;
			for (int i = 0; i < collisionRayCount; i++)
			{
				float angle = i * 360f / collisionRayCount;
				var direction = Quaternion.Euler(0, angle, 0) * transform.forward;

				if (!Physics.Raycast(transform.position, direction, out var hit, characterColliderSize,
						collisionLayerMask)) continue;
				offset += -direction * (characterColliderSize - hit.distance);
			}

			offset /= collisionRayCount / 2f;
			transform.Translate(offset, Space.World);
		}
	}
}