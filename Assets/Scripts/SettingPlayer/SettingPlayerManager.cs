using System;
using System.Collections.Generic;
using MagicCombat.Player;
using MagicCombat.Shared.GameState;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MagicCombat.SettingPlayer
{
	public class SettingPlayerManager : BaseManager
	{
		[SerializeField]
		private int minPlayers = 2;

		[SerializeField]
		private int maxPlayers = 4;
		
		public event Action OnRefreshPlayers;

		[ReadOnly]
		public List<PlayerData> playersData;

		public void RefreshPlayers(List<PlayerData> newPlayersData)
		{
			playersData = newPlayersData;
			OnRefreshPlayers?.Invoke();
		}

		public void ConfirmPlayers()
		{
			int currentPlayers = playersData.Count;
			if (currentPlayers < minPlayers || currentPlayers > maxPlayers)
				return;

			Debug.Log("Finished Setting Players");
			sharedScriptable.ProjectScenes.GoToSettingAbilities();
		}
	}
}