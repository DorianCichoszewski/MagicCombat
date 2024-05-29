using MagicCombat.Gameplay.Abilities;
using MagicCombat.Gameplay.Player;
using Shared.Data;
using Shared.Extension;
using Shared.GameState;
using Shared.Services;
using UnityEngine;

namespace MagicCombat.Gameplay
{
	public class GameplayManager : BaseManager
	{
		public PlayerAvatar CreatePlayer(UserId id)
		{
			var abilitiesData = ScriptableLocator.Get<AbilitiesContext>().AbilitiesData;
			var user = ScriptableLocator.Get<PlayerProvider>().GetUser(id);
			var staticData = ScriptableLocator.Get<StaticUserDataGroup>().Get(id);
			var playerPrefab = ScriptableLocator.Get<GameplayRuntimeData>().PlayerPrefab;

			var player = Instantiate(playerPrefab, staticData.spawnPos.ToVec3(), Quaternion.identity);
			player.Init(abilitiesData.GetOrCreate(id), staticData,
				user.GameplayInputMapping, id);
			return player;
		}
	}
}