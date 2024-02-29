using System;
using Gameplay.Abilities;

namespace Player
{
	[Serializable]
	public struct PlayerData
	{
		public PlayerController playerController;
		
		// Current Abilities
		public BaseAbility utility;
		public BaseAbility skill1;
		public BaseAbility skill2;
		public BaseAbility skill3;

		public PlayerData(PlayerController controller)
		{
			playerController = controller;
				
			utility = null;
			skill1 = null;
			skill2 = null;
			skill3 = null;
		}
	}
}