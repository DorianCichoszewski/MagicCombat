using System;
using System.Collections.Generic;
using System.Linq;
using Shared.Data;
using Shared.Interfaces;
using Sirenix.OdinInspector;
using Random = UnityEngine.Random;

namespace MagicCombat.User
{
	[Serializable]
	internal class UsersCoordinator
	{
		[ShowInInspector]
		private Dictionary<UserId, IUser> currentUsers = new();

		private List<IUser> bots = new();

		private int nextUserId;

		public int UsersCount => currentUsers.Count;
		public IEnumerable<UserId> PlayersEnumerator => currentUsers.Keys;

		public IUser GetUser(UserId id) => currentUsers[id];

		public bool TryReplaceBotWithPlayer(PlayerInputController inputController, out IUser newPlayer)
		{
			if (bots.Count > 0)
			{
				var bot = bots[^1];
				bots.RemoveAt(bots.Count - 1);
				bot.Destroy();
				newPlayer = new PlayerUser(inputController, bot);
				currentUsers[bot.Id] = newPlayer;
				return true;
			}

			newPlayer = null;
			return false;
		}

		public IUser AddPlayer(PlayerInputController inputController)
		{
			var newPlayer = new PlayerUser(inputController, GetId());
			currentUsers.Add(newPlayer.Id, newPlayer);
			return newPlayer;
		}

		public IUser AddBot()
		{
			var newBot = new BotUser(GetId());
			currentUsers.Add(newBot.Id, newBot);
			bots.Add(newBot);
			return newBot;
		}

		public IUser ReplacePlayerWithBot(UserId id)
		{
			var player = currentUsers[id];
			player.Destroy();
			var newBot = new BotUser(player);
			currentUsers[id] = newBot;
			bots.Add(newBot);
			return newBot;
		}

		public void RemoveEntity(UserId id)
		{
			var entity = currentUsers[id];
			currentUsers.Remove(id);
			if (entity is BotUser bot) bots.Remove(bot);
			entity.Destroy();
		}

		public UserId GetRandomUser()
		{
			int randomIndex = Random.Range(0, currentUsers.Count);
			return currentUsers.Keys.ToArray()[randomIndex];
		}

		public void Reset()
		{
			currentUsers.Clear();
			bots.Clear();
			nextUserId = 0;
		}

		private UserId GetId()
		{
			var newId = new UserId(++nextUserId);

			// Currently order of id's can't be changed from internalId
			newId.ChangeOrderId(nextUserId - 1);
			return newId;
		}
	}
}