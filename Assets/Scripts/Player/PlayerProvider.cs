using System;
using System.Collections.Generic;
using System.Linq;
using MagicCombat.Shared.Data;
using MagicCombat.Shared.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MagicCombat.Player
{
	[Serializable]
	public class PlayerProvider : MonoBehaviour, IPlayerProvider
	{
		[SerializeField]
		[Required]
		private StaticPlayerDataGroup defaultStaticData;

		private Dictionary<PlayerId, IPlayerInputController> inputControllers = new();

		public void AddNewPlayer(PlayerInputController inputController)
		{
			inputControllers.TryAdd(inputController.Id, inputController);
			OnPlayerChanged?.Invoke(inputController.Id);
		}

		public void RemovePlayer(PlayerId id)
		{
			inputControllers.Remove(id);
			OnPlayerChanged?.Invoke(id);
		}

		public int PlayersCount => inputControllers.Count;
		public event Action<PlayerId> OnPlayerChanged;
		public IEnumerable<PlayerId> PlayersEnumerator => inputControllers.Keys;

		public void ClearCallbacks()
		{
			OnPlayerChanged = null;
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