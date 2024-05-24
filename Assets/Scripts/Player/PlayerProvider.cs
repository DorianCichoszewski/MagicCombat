using System;
using System.Collections.Generic;
using System.Linq;
using MagicCombat.Player.Bot;
using Shared.Data;
using Shared.Interfaces;
using Shared.Notification;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MagicCombat.Player
{
	[Serializable]
	public class PlayerProvider : MonoBehaviour, IPlayerProvider
	{
		[SerializeField]
		private EventChannelPlayer addedPlayerChannel;

		[SerializeField]
		private EventChannelPlayer removedPlayerChannel;

		[SerializeField]
		[Required]
		private BotInputController botController;

		[SerializeField]
		[Required]
		private StaticPlayerDataGroup defaultStaticData;

		private Dictionary<PlayerId, IPlayerInputController> inputControllers = new();

		public void AddNewPlayer(IPlayerInputController inputController)
		{
			inputControllers.TryAdd(inputController.Id, inputController);
			addedPlayerChannel.Invoke(inputController.Id);
		}

		public void RemovePlayer(PlayerId id)
		{
			inputControllers.Remove(id);
			removedPlayerChannel.Invoke(id);
		}

		public int PlayersCount => inputControllers.Count;

		public void AddBot()
		{
			// Automatically add bot thanks to Input System
			Instantiate(botController);
		}

		public IEnumerable<PlayerId> PlayersEnumerator => inputControllers.Keys;

		public void ClearCallbacks()
		{
			addedPlayerChannel.Clear();
		}

		public PlayerId GetRandomPlayer()
		{
			int randomIndex = Random.Range(0, inputControllers.Count());
			var enumerator = inputControllers.GetEnumerator();
			while (randomIndex > 0)
			{
				enumerator.MoveNext();
				randomIndex--;
			}

			return enumerator.Current.Key;
		}

		public StaticPlayerData StaticData(PlayerId id)
		{
			return defaultStaticData.staticPlayerDatas[id.OrderedId];
		}

		public IPlayerInputController InputController(PlayerId id)
		{
			return inputControllers[id];
		}

		public IGameplayInputController GameplayInputController(PlayerId id)
		{
			return inputControllers[id].GameplayInputController;
		}
	}
}