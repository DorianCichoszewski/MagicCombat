using GameState;
using Player;
using UI;
using UnityEngine;

namespace SettingAbilities
{
	public class AbilitiesPlayerWindow : MonoBehaviour
	{
		[SerializeField]
		private GameObject firstElement;
		[SerializeField]
		private PlayerHeader header;
		[SerializeField]
		private ReadyToggle readyToggle;

		[SerializeField]
		private AbilityPicker skill1Picker;
		[SerializeField]
		private AbilityPicker skill2Picker;
		[SerializeField]
		private AbilityPicker skill3Picker;

		private SettingAbilitiesUI settingAbilitiesUI;

		public bool IsReady => readyToggle.isOn;

		public void Init(PlayerController controller, SettingAbilitiesUI ui, RuntimeScriptable runtimeScriptable)
		{
			settingAbilitiesUI = ui;
			var eventSystem = controller.EventSystem;
			eventSystem.playerRoot = gameObject;
			eventSystem.firstSelectedGameObject = firstElement;
			eventSystem.SetSelectedGameObject(firstElement);

			header.Init(controller.Data);
			readyToggle.onValueChanged.AddListener(VerifyWindowData);

			skill1Picker.Init(newSkill => runtimeScriptable.GetPlayerData(controller).skill1 = newSkill,
				runtimeScriptable.GetPlayerData(controller).skill1);
			skill2Picker.Init(newSkill => runtimeScriptable.GetPlayerData(controller).skill2 = newSkill,
				runtimeScriptable.GetPlayerData(controller).skill2);
			skill3Picker.Init(newSkill => runtimeScriptable.GetPlayerData(controller).skill3 = newSkill,
				runtimeScriptable.GetPlayerData(controller).skill3);
		}
		
		public void VerifyWindowData(bool isReady)
		{
			if (!isReady) return;
			
			

			settingAbilitiesUI.OnPlayerReady();
		}
	}
}