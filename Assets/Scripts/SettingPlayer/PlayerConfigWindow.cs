using Player;
using TMPro;
using UI;
using UnityEngine;

namespace SettingPlayer
{
	public class PlayerConfigWindow : MonoBehaviour
	{
		[SerializeField]
		private GameObject firstElement;
		
		[SerializeField]
		private PlayerHeader header;
		[SerializeField]
		private TMP_Text controllerType;

		[SerializeField]
		private ReadyToggle readyToggle;
		
		public bool IsReady => readyToggle.isOn;

		public void SetPlayer(PlayerController player, GameStartUI ui)
		{
			header.Init(player.InitData);
			controllerType.text = player.Input.devices[0].name;
			
			var eventSystem = player.EventSystem;
			eventSystem.playerRoot = gameObject;
			eventSystem.firstSelectedGameObject = firstElement;
			eventSystem.SetSelectedGameObject(firstElement);
			
			readyToggle.onValueChanged.AddListener(_ => ui.OnPlayerReady());
		}
	}
}