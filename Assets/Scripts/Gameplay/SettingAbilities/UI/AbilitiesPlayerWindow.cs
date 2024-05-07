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
			var abilitiesContext = gameplayData.AbilitiesContext;

			int points = gameplayData.points.GetOrCreate(playerId);
			pointsText.text = points > 0 ? $"Current points: {points}" : string.Empty;
			
			var collection = abilitiesContext.AbilitiesCollection;
			var abilitiesData = gameplayData.abilitiesData;
			var playerAbilities = abilitiesData.GetOrCreate(playerId);
			skill1Picker.Init(collection, newSkill => abilitiesData[playerId].Skill1Key = collection.GetKey(newSkill),
				collection.GetIndex(playerAbilities.Skill1Key));
			skill2Picker.Init(collection, newSkill => abilitiesData[playerId].Skill2Key = collection.GetKey(newSkill),
				collection.GetIndex(playerAbilities.Skill2Key));
			skill3Picker.Init(collection, newSkill => abilitiesData[playerId].Skill3Key = collection.GetKey(newSkill),
				collection.GetIndex(playerAbilities.Skill3Key));
		}
	}
}