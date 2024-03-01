using GameState;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace SettingAbilities
{
	public class AbilitiesPlayerWindow : MonoBehaviour
	{
		[SerializeField]
		private Selectable firstElement;

		private RuntimeScriptable runtimeScriptable;
		private PlayerController assignedPlayer;

		private GameObject PlayerRoot => gameObject;

		public void Init(PlayerController controller, RuntimeScriptable scriptable)
		{
			runtimeScriptable = scriptable;
			assignedPlayer = controller;
			var eventSystem = controller.EventSystem;
			eventSystem.SetSelectedGameObject(firstElement.gameObject);
			eventSystem.playerRoot = PlayerRoot;
		}
	}
}