using GameState;
using UnityEngine;

namespace SettingPlayer
{
	public class SettingPlayerState : BaseState
	{
		[SerializeField]
		private int minPlayers = 2;
		[SerializeField]
		private int maxPlayers = 4;

		public int MinPlayers => minPlayers;
		public int MaxPlayers => maxPlayers;

		public void ConfirmPlayers()
		{
			Debug.Log("GONext");
		}
	}
}