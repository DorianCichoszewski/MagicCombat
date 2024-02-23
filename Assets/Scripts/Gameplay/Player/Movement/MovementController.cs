using Extension;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Player.Basic
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
		public float targetRotationAngle;

		
		public Vector2 lastMoveDirection;
		public Vector2 moveValue;

		private IMovement currentMovement;

		public bool Enabled { get; set; } = true;
		
		public GameplayGlobals GameplayGlobals { get; private set; }

		public void Init(GameplayGlobals gameplayGlobals)
		{
			ResetMovement();

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
			var stickInput = ctx.ReadValue<Vector2>();
			if (stickInput == Vector2.zero) return;
			targetRotationAngle = Mathf.Rad2Deg * Mathf.Atan2(stickInput.x, stickInput.y);
		}

		public void Dash(float speedMultiplier, float duration)
		{
			var dash = new DashMovement(speedMultiplier, duration);
			SetMovement(dash);
		}

		private void HandleMovement(float deltaTime)
		{
			if (!Enabled)
			{
				AvoidObstacles();
				return;
			}
			
			var targetRotation = Quaternion.Euler(0f, targetRotationAngle, 0f);
			transform.rotation = targetRotation;

			currentMovement.Update(deltaTime);
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

		public void ApplyMovement(Vector2 targetMovement)
		{
			// Split big movements to better check collision
			int movementSegmentCount = (int)(targetMovement.magnitude / (characterColliderSize * 0.8)) + 1;
			var movementSegment = targetMovement * (1.0f / movementSegmentCount);

			for (int i = 0; i < movementSegmentCount; i++)
			{
				transform.Translate2(movementSegment);
				AvoidObstacles();
			}
		}

		// Public for i.e. root
		public void SetMovement<T>(T movement) where T : IMovement
		{
			currentMovement = movement;
			currentMovement.Init(this);
		}

		public void ResetMovement()
		{
			SetMovement(new DefaultMovement());
		}
	}
}