using MagicCombat.Gameplay.Abilities;
using Sirenix.OdinInspector;

namespace MagicCombat.Gameplay
{
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

		public AbilityPlayerData(StartAbilitiesData startAbilitiesData)
		{
			UtilityKey = startAbilitiesData.StartUtility;
			Skill1Key = startAbilitiesData.StartSkill1;
			Skill2Key = startAbilitiesData.StartSkill2;
			Skill3Key = startAbilitiesData.StartSkill3;
		}
	}
}