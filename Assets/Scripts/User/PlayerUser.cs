using Shared.Data;
using Shared.Interfaces;
using UnityEngine;

namespace MagicCombat.User
{
	internal class PlayerUser : IUser
	{
		public string InputName => $"Player {(int)Id}";
		public UserId Id { get; }
		public GameplayInputMapping GameplayInputMapping { get; }
		private PlayerInputController InputController { get; }

		internal PlayerUser(PlayerInputController inputController, UserId id)
		{
			InputController = inputController;
			Id = id;

			GameplayInputMapping = new GameplayInputMapping();
			inputController.PlayerGameplayInputHandler.InitMapping(GameplayInputMapping);
		}

		internal PlayerUser(PlayerInputController inputController, IUser previousUser)
		{
			InputController = inputController;
			Id = previousUser.Id;

			GameplayInputMapping = previousUser.GameplayInputMapping;
			inputController.PlayerGameplayInputHandler.InitMapping(GameplayInputMapping);
		}

		public void SetUIFocus(GameObject root, GameObject firstSelected, GameObject readyToggle = null)
		{
			InputController.SetUIFocus(root, firstSelected, readyToggle);
		}

		public void Destroy()
		{
			Object.Destroy(InputController.gameObject);
		}
	}
}