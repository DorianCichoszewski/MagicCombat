using Player;
using TMPro;
using UnityEngine;

namespace SettingPlayer
{
	public class PlayerConfigWindow : MonoBehaviour
	{
		[SerializeField]
		private TMP_Text header;
		[SerializeField]
		private TMP_Text controllerType;

		public void SetPlayer(PlayerController player)
		{
			var data = player.Data;
			header.text = data.name;
			header.color = data.color;
			controllerType.text = player.Input.devices[0].name;
		}
	}
}