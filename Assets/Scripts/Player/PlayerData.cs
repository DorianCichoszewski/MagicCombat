using System;
using Gameplay.Abilities;

namespace Player
{
	[Serializable]
	public class PlayerData
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
				
			utility = controller.Data.startUtility;
			skill1 = controller.Data.startSkill1;
			skill2 = controller.Data.startSkill2;
			skill3 = controller.Data.startSkill3;
		}
	}
}