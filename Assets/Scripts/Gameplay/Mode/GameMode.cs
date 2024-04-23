using System.Collections.Generic;
using MagicCombat.Gameplay.Player;
using MagicCombat.Shared.GameState;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MagicCombat.Gameplay.Mode
{
	public abstract class GameMode : ScriptableObject
	{
		[ShowInInspector]
		[ReadOnly]
		protected List<PlayerAvatar> alivePlayers = new();

		public List<PlayerAvatar> AlivePlayers => alivePlayers;

		public abstract bool GameInProgress { get; }

		public abstract void Init(SharedScriptable shared, GameplayManager manager);
		public abstract void PlayerHit(PlayerAvatar player);

		public abstract void SimulateGame();
	}
}