using System;
using MagicCombat.Gameplay.Abilities;

namespace MagicCombat.Gameplay
{
	[Serializable]
	public class GameplayPlayerData
	{
		public AbilitiesGroup AbilitiesGroup { get; }

		// Current Abilities
		public int UtilityIndex { get; set; }
		public int Skill1Index { get; set; }
		public int Skill2Index { get; set; }
		public int Skill3Index { get; set; }

		public GameplayPlayerData(StartAbilitiesData startAbilitiesData)
		{
			AbilitiesGroup = startAbilitiesData.AbilitiesGroup;
			UtilityIndex = startAbilitiesData.StartUtilityIndex;
			Skill1Index = startAbilitiesData.StartSkill1Index;
			Skill2Index = startAbilitiesData.StartSkill2Index;
			Skill3Index = startAbilitiesData.StartSkill3Index;
		}
	}
}