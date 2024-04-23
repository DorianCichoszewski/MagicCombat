using MagicCombat.Gameplay;
using MagicCombat.UI.Shared;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace MagicCombat.SettingAbilities.UI
{
	public class AbilitiesPlayerWindow : PerPlayerWindow
	{
		[SerializeField]
		[Required]
		private PlayerHeader header;

		[SerializeField]
		[Required]
		private TMP_Text pointsText;

		[Header("Skills")]
		[SerializeField]
		[Required]
		private AbilityPicker skill1Picker;

		[SerializeField]
		[Required]
		private AbilityPicker skill2Picker;

		[SerializeField]
		[Required]
		private AbilityPicker skill3Picker;

		protected override void OnInit()
		{
			header.Init(PlayerProvider.StaticData(playerId));

			var gameplayData = (GameplayRuntimeData)sharedScriptable.ModeData;

			int points = gameplayData.points.GetOrCreate(playerId);

			pointsText.text = points > 0 ? $"Current points: {points}" : string.Empty;

			var playerData = gameplayData.playerData.GetOrCreate(playerId);
			skill1Picker.Init(playerData.AbilitiesGroup, newSkill => playerData.Skill1Index = newSkill,
				playerData.Skill1Index);
			skill2Picker.Init(playerData.AbilitiesGroup, newSkill => playerData.Skill2Index = newSkill,
				playerData.Skill2Index);
			skill3Picker.Init(playerData.AbilitiesGroup, newSkill => playerData.Skill3Index = newSkill,
				playerData.Skill3Index);
		}
	}
}