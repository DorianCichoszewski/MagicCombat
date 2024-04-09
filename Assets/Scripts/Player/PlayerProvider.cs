using System;
using System.Collections.Generic;
using MagicCombat.Shared.Data;
using MagicCombat.Shared.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MagicCombat.Player
{
	[Serializable]
	public class PlayerProvider : MonoBehaviour, IPlayerProvider
	{
		[SerializeField]
		[Required]
		private StaticPlayerDataGroup defaultStaticData;

		private Dictionary<int, IPlayerInputController> inputControllers = new();
		
		public void AddNewPlayer(PlayerInputController inputController)
		{
			inputControllers.TryAdd(inputController.Index, inputController);
		}

		public void RemovePlayer(int id)
		{
			inputControllers.Remove(id); 
		}

		public int PlayersCount => inputControllers.Count;
		public IEnumerable<int> PlayersIdEnumerator => inputControllers.Keys;

		public StaticPlayerData StaticData(int id)
		{
			return defaultStaticData.staticPlayerDatas[id];
		}

		public IPlayerInputController InputController(int id)
		{
			return inputControllers[id];
		}

		public IGameplayInputController GameplayInputController(int id)
		{
			return inputControllers[id].GameplayInputController;
		}
	}
}