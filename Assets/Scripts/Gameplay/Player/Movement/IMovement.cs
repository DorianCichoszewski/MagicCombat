using UnityEngine;

namespace MagicCombat.Gameplay.Player.Movement
{
	public interface IMovement
	{
		public void Init(MovementController controller);

		public Vector2 Update(float deltaTime);

		public void ChangeMovement();
	}
}