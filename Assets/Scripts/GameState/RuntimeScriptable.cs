using System.Collections.Generic;
using Player;
using UnityEngine;

namespace GameState
{
	[CreateAssetMenu(menuName = "Magic Combat/Runtime Scriptable", fileName = "Runtime Scriptable")]
	public class RuntimeScriptable : ScriptableObject
	{
		#region Essentials

		[SerializeField]
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

		public void EnsureEssentials()
		{
			if (createdEssentials != null) return;
			createdEssentials = Instantiate(essentialsPrefab);
			createdEssentials.Init();
		}

		#endregion
		
		public List<PlayerData> playersData = new();

		public PlayerData GetPlayerData(PlayerController playerController)
		{
			return playersData[playerController.Index];
		}
	}
}