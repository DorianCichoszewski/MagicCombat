using System.Collections.Generic;
using System.Linq;
using MagicCombat.Player.Bot;
using Shared.Data;
using Shared.GameState;
using Shared.Interfaces;
using Shared.Notification;
using Shared.Services;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MagicCombat.Player
{
	[CreateAssetMenu(menuName = "Single/Startup/Player Provider Couch Multiplayer", fileName = "Player Provider Couch Multiplayer")]
	public class PlayerProviderCouch : PlayerProvider
	{
		[SerializeField]
		[Required]
		private EventChannelPlayer addedPlayerChannel;

		[SerializeField]
		[Required]
		private EventChannelPlayer removedPlayerChannel;

		[Space]
		[SerializeField]
		[Required]
		private PlayersManager playersManager;
		
		[SerializeField]
		[Required]
		private BotInputController botController;

		private Dictionary<PlayerId, IPlayerInputController> inputControllers = new();
		
		public override int PlayersCount => inputControllers.Count;
		
		public override IEnumerable<PlayerId> PlayersEnumerator => inputControllers.Keys;

		public override void GameStart()
		{
			ScriptableLocator.RegisterService<PlayerProvider>(this);
			ClearCallbacks();
			Instantiate(playersManager, Vector3.zero, Quaternion.identity, null).Init(this);
		}

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
		
		public override void AddBot()
		{
			// Automatically add bot thanks to Input System
			Instantiate(botController);
		}

		public override void ClearCallbacks()
		{
			addedPlayerChannel.Clear();
			removedPlayerChannel.Clear();
		}

		public override PlayerId GetRandomPlayer()
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

		public override StaticPlayerData StaticData(PlayerId id)
		{
			return ScriptableLocator.Get<StaticPlayerDataGroup>().staticPlayerDatas[id.OrderedId];
		}

		public override IPlayerInputController InputController(PlayerId id)
		{
			return inputControllers[id];
		}

		public override IGameplayInputController GameplayInputController(PlayerId id)
		{
			return inputControllers[id].GameplayInputController;
		}
	}
}