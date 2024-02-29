using System;
using Gameplay.Abilities;

namespace Gameplay.Player
{
	[Serializable]
	public class PlayerState
	{
		public BaseAbility utility;
		public BaseAbility skill1;
		public BaseAbility skill2;
		public BaseAbility skill3;
	}
}