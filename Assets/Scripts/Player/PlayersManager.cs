using Shared.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MagicCombat.Player
{
	[RequireComponent(typeof(PlayerInputManager))]
	public class PlayersManager : MonoBehaviour
	{
		private PlayerProviderCouch playerProvider;
		private PlayerInputManager inputManager;
		private PlayerIdManager playerIdManager;

		public void Init(PlayerProviderCouch playerProvider)
		{
			playerIdManager = new PlayerIdManager();
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
			var controller = input.GetComponent<IPlayerInputController>();
			playerIdManager.AddPlayer(controller);
			playerProvider.AddNewPlayer(controller);
			input.transform.SetParent(transform);
		}

		private void PlayerLeft(PlayerInput input)
		{
			var controller = input.GetComponent<IPlayerInputController>();
			playerIdManager.RemovePlayer(controller);
			playerProvider.RemovePlayer(controller.Id);
			controller.Destroy();
		}
	}
}