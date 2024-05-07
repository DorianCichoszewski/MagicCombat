using MagicCombat.Gameplay.Abilities;
using MagicCombat.Gameplay.Mode;
using MagicCombat.Gameplay.Player;
using MagicCombat.Shared.Data;
using MagicCombat.Shared.Extension;
using MagicCombat.Shared.GameState;
using MagicCombat.Shared.Interfaces;
using MagicCombat.Shared.Time;
using UnityEngine;

namespace MagicCombat.Gameplay
{
	public class GameplayManager : BaseManager
	{
		[SerializeField]
		private ClockGameObject clockGO;

		public AbilitiesContext AbilitiesContext => GameModeData.AbilitiesContext;
		public GameplayRuntimeData GameModeData => (GameplayRuntimeData)sharedScriptable.ModeData;
		public GameMode Mode => GameModeData.GameMode;

		protected void Awake()
		{
			clockGO.Init(AbilitiesContext.ClockManager);

			Mode.Init(sharedScriptable, this);
		}

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