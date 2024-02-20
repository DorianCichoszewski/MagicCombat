using System.Collections.Generic;
using Gameplay.Player;
using UnityEngine;

namespace Gameplay.UI
{
	public class GameplayUI : MonoBehaviour
	{
		[SerializeField]
		private List<PlayerUI> playersUI = new ();
		
		public void PlayerSetup(PlayerBase player, int index)
		{
			playersUI[index].Init(player);
		}
	}
}