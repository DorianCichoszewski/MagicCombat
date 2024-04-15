using MagicCombat.Shared.Data;
using MagicCombat.Shared.Interfaces;
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

		public void SetPlayer(IPlayerProvider playerProvider, PlayerId id, SettingPlayersUI ui)
		{
			var staticData = playerProvider.StaticData(id);
			header.Init(staticData);
			controllerType.text = staticData.name;

			playerProvider.InputController(id).SetUIFocus(gameObject, firstElement.gameObject);

			readyToggle.onValueChanged.AddListener(_ => ui.OnPlayerReady());
		}
	}
}