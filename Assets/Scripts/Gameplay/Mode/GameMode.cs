using System.Collections.Generic;
using System.Linq;
using MagicCombat.Gameplay.Player;
using Shared.Data;
using Shared.GameState;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MagicCombat.Gameplay.Mode
{
	public abstract class GameMode : ScriptableObject
	{
		[ShowInInspector]
		[ReadOnly]
		protected List<PlayerAvatar> alivePlayers;

		public List<PlayerAvatar> AlivePlayers => alivePlayers;
		
		public PlayerAvatar GetPlayer(PlayerId id) => alivePlayers.First(player => player.Id == id);

		public abstract bool GameInProgress { get; }

		public abstract void Run(SharedScriptable shared, GameplayManager manager);

		public abstract void PlayerHit(PlayerAvatar player);

		public abstract void SimulateGame();
	}
}