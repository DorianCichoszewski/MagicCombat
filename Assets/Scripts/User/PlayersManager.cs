using UnityEngine;
using UnityEngine.InputSystem;

namespace MagicCombat.User
{
	[RequireComponent(typeof(PlayerInputManager))]
	internal class PlayersManager : MonoBehaviour
	{
		private PlayerProviderCouch playerProvider;
		private PlayerInputManager inputManager;

		public void Init(PlayerProviderCouch playerProvider)
		{
			inputManager = GetComponent<PlayerInputManager>();
			this.playerProvider = playerProvider;

			inputManager.onPlayerJoined += PlayerJoined;
			inputManager.onPlayerLeft += PlayerLeft;

			transform.SetParent(null);
			DontDestroyOnLoad(gameObject);
		}

		public void SetJoining(bool value)
		{
			if (value)
				inputManager.EnableJoining();
			else
				inputManager.DisableJoining();
		}

		private void PlayerJoined(PlayerInput input)
		{
			var controller = input.GetComponent<PlayerInputController>();
			input.transform.SetParent(transform);

			playerProvider.AddNewPlayer(controller);
		}

		private void PlayerLeft(PlayerInput input)
		{
			var controller = input.GetComponent<PlayerInputController>();
			playerProvider.RemoveUser(controller.Id);
			controller.Destroy();
		}
	}
}