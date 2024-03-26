using UnityEngine;

namespace MagicCombat.Gameplay.Avatar.Movement
{
	public interface IAvatarMovement
	{
		public void Init(MovementController controller);

		public Vector2 Update(float deltaTime);

		public void ChangeMovement();
	}
}