using MagicCombat.Player;
using MagicCombat.UI.Shared;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MagicCombat.SettingAbilities.UI
{
	public class AbilitiesPlayerWindow : MonoBehaviour
	{
		[SerializeField]
		[Required]
		private Selectable firstElement;

		[SerializeField]
		[Required]
		private PlayerHeader header;

		[SerializeField]
		[Required]
		private ReadyToggle readyToggle;

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

		private SettingAbilitiesUI settingAbilitiesUI;
		
		public bool IsReady => readyToggle.isOn;

		public void Init(PlayerData playerData, SettingAbilitiesUI ui)
		{
			settingAbilitiesUI = ui;
			playerData.playerInputController.SetUIFocus(gameObject, firstElement.gameObject);
			
			header.Init(playerData.staticData);
			readyToggle.onValueChanged.AddListener(VerifyWindowData);
			
			pointsText.text = playerData.points > 0 ? $"Current points: {playerData.points}" : string.Empty;

			var abilitiesGroup = playerData.gameplay.AbilitiesGroup;
			var gameplayData = playerData.gameplay;
			skill1Picker.Init(abilitiesGroup, newSkill => gameplayData.Skill1Index = newSkill, gameplayData.Skill1Index);
			skill2Picker.Init(abilitiesGroup, newSkill => gameplayData.Skill2Index = newSkill, gameplayData.Skill2Index);
			skill3Picker.Init(abilitiesGroup, newSkill => gameplayData.Skill3Index = newSkill, gameplayData.Skill3Index);
		}

		private void VerifyWindowData(bool isReady)
		{
			if (!isReady) return;

			// TODO: Check for empty / null skills

			settingAbilitiesUI.OnPlayerReady();
		}
	}
}