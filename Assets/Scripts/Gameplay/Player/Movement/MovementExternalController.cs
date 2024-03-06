using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Player.Movement
{
	public class MovementExternalController
	{
		private List<MovementForce> currentForces = new ();

		public void AddForce(MovementForce newForce)
		{
			currentForces.Add(newForce);
		}

		public Vector2 Update(float deltaTime)
		{
			for (int i = currentForces.Count - 1; i >= 0; i--)
			{
				if (!currentForces[i].IsActive)
					currentForces.RemoveAt(i);
			}
			
			Vector2 finalMovement = Vector2.zero;

			foreach (var force in currentForces)
			{
				finalMovement += force.GetForce(deltaTime);
			}

			return finalMovement;
		}
	}
}