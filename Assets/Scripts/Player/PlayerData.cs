using System;
using Gameplay.Abilities;
using Sirenix.OdinInspector;

namespace MagicCombat.Player
{
	[Serializable]
	public class PlayerData
	{
		[ReadOnly]
		public PlayerController controller;

		// Current Abilities
		public BaseAbility utility;
		public BaseAbility skill1;
		public BaseAbility skill2;
		public BaseAbility skill3;

		public int points;

		public PlayerData(PlayerController controller)
		{
			this.controller = controller;

			utility = controller.StartData.startUtility;
			skill1 = controller.StartData.startSkill1;
			skill2 = controller.StartData.startSkill2;
			skill3 = controller.StartData.startSkill3;
		}
	}
}