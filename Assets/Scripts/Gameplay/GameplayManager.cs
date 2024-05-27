using MagicCombat.Gameplay.Abilities;
using MagicCombat.Gameplay.Player;
using Shared.Data;
using Shared.Extension;
using Shared.GameState;
using Shared.Interfaces;
using Shared.Services;
using UnityEngine;

namespace MagicCombat.Gameplay
{
	public class GameplayManager : BaseManager
	{

		public PlayerAvatar CreatePlayer(StaticPlayerData staticData, IGameplayInputController input, PlayerId id)
		{
			var gameplayRuntimeData = ScriptableLocator.Get<GameplayRuntimeData>();
			var abilitiesContext = ScriptableLocator.Get<AbilitiesContext>();
			var playerPrefab = gameplayRuntimeData.PlayerPrefab;
			var player = Instantiate(playerPrefab, staticData.spawnPos.ToVec3(), Quaternion.identity);
			player.Init(abilitiesContext.AbilitiesData.GetOrCreate(id), staticData, input, id);
			return player;
		}
	}
}