using System.Collections.Generic;
using Player;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameState
{
	[CreateAssetMenu(menuName = "Magic Combat/Runtime Scriptable", fileName = "Runtime Scriptable")]
	public class RuntimeScriptable : ScriptableObject
	{
		#region Essentials

		[SerializeField, AssetsOnly, Required]
		private GlobalState essentialsPrefab;

		private GlobalState createdEssentials;

		public GlobalState Essentials
		{
			get
			{
				EnsureEssentials();
				return createdEssentials;
			}
		}

		public PlayersManager PlayersManager => Essentials.playersManager;
		public ProjectScenes ProjectScenes => Essentials.projectScenes;

		public void EnsureEssentials()
		{
			if (createdEssentials != null) return;
			createdEssentials = Instantiate(essentialsPrefab);
			createdEssentials.Init();
		}

		#endregion
		
		public List<PlayerData> playersData = new();
		
		public void AddPlayerData(PlayerController playerController)
		{
			foreach (var playerData in playersData)
			{
				if (playerData.controller == null)
				{
					playerData.controller = playerController;
					return;
				}
			}
			playersData.Add(new PlayerData(playerController));
		}

		public PlayerData GetPlayerData(PlayerController playerController)
		{
			return playersData[playerController.Index];
		}
	}
}