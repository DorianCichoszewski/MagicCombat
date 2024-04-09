using MagicCombat.Gameplay;
using MagicCombat.Shared.Interfaces;
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

		public void Init(IPlayerProvider playerProvider, GameplayRuntimeData gameplayData, int id,
			SettingAbilitiesUI ui)
		{
			settingAbilitiesUI = ui;
			var inputController = playerProvider.InputController(id);
			inputController.SetUIFocus(gameObject, firstElement.gameObject);

			header.Init(playerProvider.StaticData(id));
			readyToggle.onValueChanged.AddListener(VerifyWindowData);

			int points = gameplayData.points.GetOrCreate(id);

			pointsText.text = points > 0 ? $"Current points: {points}" : string.Empty;

			var playerData = gameplayData.playerData.GetOrCreate(id);
			skill1Picker.Init(playerData.AbilitiesGroup, newSkill => playerData.Skill1Index = newSkill,
				playerData.Skill1Index);
			skill2Picker.Init(playerData.AbilitiesGroup, newSkill => playerData.Skill2Index = newSkill,
				playerData.Skill2Index);
			skill3Picker.Init(playerData.AbilitiesGroup, newSkill => playerData.Skill3Index = newSkill,
				playerData.Skill3Index);
		}

		private void VerifyWindowData(bool isReady)
		{
			if (!isReady) return;

			// TODO: Check for empty / null skills

			settingAbilitiesUI.OnPlayerReady();
		}
	}
}