using System;
using MagicCombat.Gameplay.Abilities;
using Sirenix.OdinInspector;

namespace MagicCombat.Gameplay
{
	[Serializable]
	public class AbilityPlayerData
	{
		[ShowInInspector]
		public string UtilityKey { get; set; }
		[ShowInInspector]
		public string Skill1Key { get; set; }
		[ShowInInspector]
		public string Skill2Key { get; set; }
		[ShowInInspector]
		public string Skill3Key { get; set; }

		public AbilityPlayerData(InitialAbilities initialAbilities)
		{
			UtilityKey = initialAbilities.StartUtility;
			Skill1Key = initialAbilities.StartSkill1;
			Skill2Key = initialAbilities.StartSkill2;
			Skill3Key = initialAbilities.StartSkill3;
		}
	}
}