using System;
using System.Collections.Generic;
using MagicCombat.Shared.GameState;
using MagicCombat.Shared.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MagicCombat.Player
{
	[RequireComponent(typeof(PlayerInputManager), typeof(PlayerProvider))]
	public class PlayersManager : MonoBehaviour, IEssentialScript
	{
		[SerializeField]
		[Required]
		private StaticPlayerDataGroup dataGroup;

		[SerializeField]
		[ReadOnly]
		private List<PlayerInputController> players;

		private PlayerInputManager inputManager;
		private PlayerProvider playerProvider;

		public List<PlayerInputController> Players => players;

		public event Action<PlayerInputController> onPlayerJoined;
		public event Action<PlayerInputController> onPlayerLeft;

		public void Init(SharedScriptable sharedScriptable)
		{
			inputManager = GetComponent<PlayerInputManager>();
			playerProvider = GetComponent<PlayerProvider>();
			inputManager.onPlayerJoined += PlayerJoined;
			inputManager.onPlayerLeft += PlayerLeft;
		}

		public void ClearEvents()
		{
			onPlayerJoined = null;
			onPlayerLeft = null;
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
			players.Insert(controller.Index, controller);
			playerProvider.AddNewPlayer(controller);
			input.transform.SetParent(transform);
			onPlayerJoined?.Invoke(controller);
		}

		private void PlayerLeft(PlayerInput input)
		{
			var controller = input.GetComponent<PlayerInputController>();
			players.Remove(controller);
			playerProvider.RemovePlayer(controller.Index);
			onPlayerLeft?.Invoke(controller);
		}
	}
}