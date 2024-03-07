using MagicCombat.GameState;
using MagicCombat.Player;
using MagicCombat.UI.Shared;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MagicCombat.UI.SettingAbilities
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

		public GameObject FirstElement => firstElement.gameObject;
		public bool IsReady => readyToggle.isOn;

		public void Init(PlayerController controller, SettingAbilitiesUI ui, RuntimeScriptable runtimeScriptable)
		{
			settingAbilitiesUI = ui;
			var eventSystem = controller.EventSystem;
			eventSystem.playerRoot = gameObject;
			eventSystem.firstSelectedGameObject = FirstElement;
			eventSystem.SetSelectedGameObject(FirstElement);

			header.Init(controller.InitData);
			readyToggle.onValueChanged.AddListener(VerifyWindowData);
			var playerData = runtimeScriptable.GetPlayerData(controller);
			pointsText.text = playerData.points > 0 ? $"Current points: {playerData.points}" : string.Empty;

			skill1Picker.Init(newSkill => playerData.skill1 = newSkill, playerData.skill1);
			skill2Picker.Init(newSkill => playerData.skill2 = newSkill, playerData.skill2);
			skill3Picker.Init(newSkill => playerData.skill3 = newSkill, playerData.skill3);
		}

		private void VerifyWindowData(bool isReady)
		{
			if (!isReady) return;

			// TODO: Check for empty / null skills

			settingAbilitiesUI.OnPlayerReady();
		}
	}
}