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
		private GameplayRuntimeData gameplayRuntimeData;
		
		public AbilitiesContext AbilitiesContext => gameplayRuntimeData.AbilitiesContext;
		public GameplayRuntimeData GameplayRuntimeData => gameplayRuntimeData;

		public PlayerAvatar CreatePlayer(StaticPlayerData staticData, IGameplayInputController input, PlayerId id)
		{
			gameplayRuntimeData = ScriptableLocator.Get<GameplayRuntimeData>();
			var playerPrefab = gameplayRuntimeData.PlayerPrefab;
			var player = Instantiate(playerPrefab, staticData.spawnPos.ToVec3(), Quaternion.identity);
			player.Init(gameplayRuntimeData.abilitiesData.GetOrCreate(id), staticData, this, input, id);
			return player;
		}
	}
}