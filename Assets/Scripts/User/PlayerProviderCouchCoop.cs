using System.Collections.Generic;
using Shared.Data;
using Shared.GameState;
using Shared.Interfaces;
using Shared.Notification;
using Shared.Services;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MagicCombat.User
{
	[CreateAssetMenu(menuName = "Single/Startup/Player Provider Couch Multiplayer",
		fileName = "Player Provider Couch Multiplayer")]
	internal class PlayerProviderCouch : PlayerProvider
	{
		[SerializeField]
		[Required]
		private EventChannelUser addedUserChannel;

		[SerializeField]
		[Required]
		private EventChannelUser removedUserChannel;

		[SerializeField]
		[Required]
		private EventChannelUser changedUserStatus;

		[Space]
		[SerializeField]
		[Required]
		private PlayersManager playersManager;

		[ShowInInspector]
		[ReadOnly]
		private UsersCoordinator usersCoordinator;

		public override IEnumerable<UserId> PlayersEnumerator => usersCoordinator.PlayersEnumerator;
		public override int PlayersCount => usersCoordinator.UsersCount;
		public override UserId GetRandomUser() => usersCoordinator.GetRandomUser();
		public override IUser GetUser(UserId id) => usersCoordinator.GetUser(id);
		
		public override void GameStart()
		{
			ScriptableLocator.RegisterService<PlayerProvider>(this);
			usersCoordinator.Reset();
			ClearCallbacks();
			Instantiate(playersManager, Vector3.zero, Quaternion.identity, null).Init(this);
		}

		public override void AddBot()
		{
			var bot = usersCoordinator.AddBot();
			addedUserChannel.Invoke(bot.Id);
		}

		public override void ClearCallbacks()
		{
			addedUserChannel.Clear();
			removedUserChannel.Clear();
			changedUserStatus.Clear();
		}

		internal void AddNewPlayer(PlayerInputController inputController)
		{
			if (usersCoordinator.TryReplaceBotWithPlayer(inputController, out var newPlayer))
			{
				changedUserStatus.Invoke(newPlayer.Id);
			}
			else
			{
				newPlayer = usersCoordinator.AddPlayer(inputController);
				addedUserChannel.Invoke(newPlayer.Id);
			}

			inputController.Id = newPlayer.Id;
		}

		public void RemoveUser(UserId id)
		{
			removedUserChannel.Invoke(id);
			usersCoordinator.RemoveEntity(id);
		}
	}
}