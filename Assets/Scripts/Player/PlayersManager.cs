using System;
using System.Collections.Generic;
using GameState;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
	[RequireComponent(typeof(PlayerInputManager))]
	public class PlayersManager : MonoBehaviour
	{
		[SerializeField]
		private RuntimeScriptable runtimeScriptable;

		[SerializeField]
		private List<PlayerController> players;
		
		private PlayerInputManager inputManager;

		public event Action<PlayerController> onPlayerJoined;
		public event Action<PlayerController> onPlayerLeft;

		public void Init()
		{
			runtimeScriptable.playersData.Clear();
			inputManager = GetComponent<PlayerInputManager>();
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
			{
				inputManager.EnableJoining();
			}
			else
			{
				inputManager.DisableJoining();
			}
		}

		private void PlayerJoined(PlayerInput input)
		{
			var controller = input.GetComponent<PlayerController>();
			runtimeScriptable.playersData.Add(new PlayerData(controller));
			players.Add(controller);
			onPlayerJoined?.Invoke(controller);
		}
		
		private void PlayerLeft(PlayerInput input)
		{
			var controller = input.GetComponent<PlayerController>();
			runtimeScriptable.playersData.Remove(new PlayerData(controller));
			players.Remove(controller);
			onPlayerLeft?.Invoke(controller);
		}
	}
}