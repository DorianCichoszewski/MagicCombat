using MagicCombat.Shared.GameState;
using MagicCombat.Shared.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MagicCombat.Player
{
	[RequireComponent(typeof(PlayerInputManager), typeof(PlayerProvider))]
	public class PlayersManager : MonoBehaviour, IEssentialScript
	{
		private PlayerProvider playerProvider;
		private PlayerInputManager inputManager;
		private PlayerIdManager playerIdManager;

		public void Init(SharedScriptable sharedScriptable)
		{
			playerIdManager = new PlayerIdManager();
			inputManager = GetComponent<PlayerInputManager>();
			playerProvider = GetComponent<PlayerProvider>();

			inputManager.onPlayerJoined += PlayerJoined;
			inputManager.onPlayerLeft += PlayerLeft;
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
			playerIdManager.AddPlayer(controller);
			playerProvider.AddNewPlayer(controller);
			input.transform.SetParent(transform);
		}

		private void PlayerLeft(PlayerInput input)
		{
			var controller = input.GetComponent<PlayerInputController>();
			playerIdManager.RemovePlayer(controller);
			playerProvider.RemovePlayer(controller.Id);
			Destroy(controller.gameObject);
		}
	}
}