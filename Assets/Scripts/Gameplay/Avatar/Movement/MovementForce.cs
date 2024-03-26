using System;
using UnityEngine;

namespace MagicCombat.Gameplay.Avatar.Movement
{
	[Serializable]
	public class MovementForce
	{
		public float duration;
		public MovementForceType type;

		private Vector2 forceVector;
		private float remainingDuration;

		public bool IsActive => remainingDuration > 0;

		public MovementForce GetNew(Vector2 forceVector)
		{
			return new MovementForce
			{
				duration = duration,
				type = type,
				forceVector = forceVector,
				remainingDuration = duration
			};
		}

		public Vector2 GetForce(float deltaTime)
		{
			deltaTime = Mathf.Min(deltaTime, remainingDuration);
			var ret = deltaTime * forceVector;
			ret *= type switch
			{
				MovementForceType.Constant => 1,
				MovementForceType.Linear => remainingDuration / duration,
				_ => throw new ArgumentOutOfRangeException()
			};
			remainingDuration -= deltaTime;
			return ret;
		}
	}

	public enum MovementForceType
	{
		Constant,
		Linear
	}
}