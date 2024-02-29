using UnityEngine;

namespace Gameplay.Player.Basic
{
	public interface IMovement
	{
		public void Init(MovementController controller);

		public Vector2 Update(float deltaTime);

		public void ChangeMovement();
	}
}