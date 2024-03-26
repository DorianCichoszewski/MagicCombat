using System;
using MagicCombat.Gameplay;
using MagicCombat.Shared.Data;
using MagicCombat.Shared.Interfaces;

namespace MagicCombat.Player
{
	[Serializable]
	public class PlayerData
	{
		public GameplayPlayerData gameplay;

		public StaticPlayerData staticData;

		public IGameplayInputController gameplayInput;

		public PlayerInputController playerInputController;

		public int points;
		public int id;
	}
}