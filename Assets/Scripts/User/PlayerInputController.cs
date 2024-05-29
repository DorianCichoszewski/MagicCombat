using Shared.Data;
using Shared.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

namespace MagicCombat.User
{
	internal class PlayerInputController : MonoBehaviour
	{
		[SerializeField]
		[Required]
		private PlayerInput playerInput;

		[SerializeField]
		[Required]
		private PlayerGameplayInputHandler playerGameplayInputHandler;

		[SerializeField]
		[Required]
		private MultiplayerEventSystem eventSystem;

		public PlayerInput PlayerInput => playerInput;
		public MultiplayerEventSystem EventSystem => eventSystem;
		public PlayerGameplayInputHandler PlayerGameplayInputHandler => playerGameplayInputHandler;

		public GameplayInputMapping GameplayInputMapping => GetComponent<GameplayInputMapping>();
		public UserId Id { get; set; }

		public void SetUIFocus(GameObject root, GameObject firstSelected, GameObject readyToggle = null)
		{
			eventSystem.playerRoot = root;
			eventSystem.firstSelectedGameObject = firstSelected;
			eventSystem.SetSelectedGameObject(firstSelected);
		}

		public void ResetSelection()
		{
			eventSystem.playerRoot = null;
			eventSystem.firstSelectedGameObject = null;
			eventSystem.SetSelectedGameObject(null);
		}

		public void Destroy()
		{
			Destroy(gameObject);
		}
	}
}