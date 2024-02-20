using System.Collections.Generic;
using Gameplay.Player;
using Gameplay.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
	public class GameplayManager : MonoBehaviour
	{
		[SerializeField]
		private StartData startData;

		[SerializeField]
		private GameplayUI gameplayUI;
		
		private List<PlayerBase> currentPlayers = new ();

		public void OnAddPlayer(PlayerInput playerInput)
		{
			int playerIndex = currentPlayers.Count;
			var player = playerInput.GetComponent<PlayerBase>();

			currentPlayers.Add(player);
			player.SetData(startData.playerInitList[playerIndex]);
			gameplayUI.PlayerSetup(player, playerIndex);
		}
	}
}