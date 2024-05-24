using MagicCombat.Gameplay.Abilities;
using MagicCombat.Gameplay.Mode;
using MagicCombat.Gameplay.Player;
using Shared.Data;
using Shared.Extension;
using Shared.GameState;
using Shared.Interfaces;
using UnityEngine;

namespace MagicCombat.Gameplay
{
	public class GameplayManager : BaseManager
	{
		public AbilitiesContext AbilitiesContext => GameModeData.AbilitiesContext;
		public GameplayRuntimeData GameModeData => (GameplayRuntimeData)sharedScriptable.ModeData;
		public GameMode Mode => GameModeData.GameMode;

		public void PlayerHit(PlayerAvatar player)
		{
			Mode.PlayerHit(player);
		}

		public PlayerAvatar CreatePlayer(StaticPlayerData staticData, IGameplayInputController input, PlayerId id)
		{
			var playerPrefab = GameModeData.PlayerPrefab;
			var player = Instantiate(playerPrefab, staticData.spawnPos.ToVec3(), Quaternion.identity);
			player.Init(GameModeData.abilitiesData.GetOrCreate(id), staticData, this, input, id);
			return player;
		}
	}
}