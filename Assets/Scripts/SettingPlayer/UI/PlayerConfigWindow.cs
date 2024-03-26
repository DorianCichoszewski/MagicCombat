using MagicCombat.Player;
using MagicCombat.UI.Shared;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MagicCombat.SettingPlayer.UI
{
	public class PlayerConfigWindow : MonoBehaviour
	{
		[SerializeField]
		private Selectable firstElement;

		[SerializeField]
		private PlayerHeader header;

		[SerializeField]
		private TMP_Text controllerType;

		[SerializeField]
		private ReadyToggle readyToggle;

		public bool IsReady => readyToggle.isOn;

		public void SetPlayer(PlayerData playerData, SettingPlayersUI ui)
		{
			header.Init(playerData.staticData);
			controllerType.text = playerData.staticData.name;
			
			playerData.playerInputController.SetUIFocus(gameObject, firstElement.gameObject);

			readyToggle.onValueChanged.AddListener(_ => ui.OnPlayerReady());
		}
	}
}