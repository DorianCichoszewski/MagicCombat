using MagicCombat.UI.Shared;
using TMPro;
using UnityEngine;

namespace MagicCombat.SettingPlayer.UI
{
	public class PlayerConfigWindow : PerPlayerWindow
	{
		[SerializeField]
		private PlayerHeader header;

		[SerializeField]
		private TMP_Text controllerType;

		protected override void OnInit()
		{
			var staticData = PlayerProvider.StaticData(playerId);
			header.Init(staticData);
			controllerType.text = staticData.name;
		}
	}
}