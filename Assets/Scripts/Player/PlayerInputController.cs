using MagicCombat.Shared.Data;
using MagicCombat.Shared.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

namespace MagicCombat.Player
{
	public class PlayerInputController : MonoBehaviour, IPlayerInputController
	{
		[SerializeField]
		private PlayerInput input;

		[SerializeField]
		private PlayerGameplayInput playerGameplayInput;

		[SerializeField]
		private MultiplayerEventSystem eventSystem;

		public PlayerId Id { get; private set; }
		public IGameplayInputController GameplayInputController => playerGameplayInput;
		public string InputName => input.devices[0].name;

		public void SetId(PlayerId playerId)
		{
			Id = playerId;
			playerId.ChangeControllerStatus(true);
		}

		public void ResetSelection()
		{
			eventSystem.playerRoot = null;
			eventSystem.firstSelectedGameObject = null;
			eventSystem.SetSelectedGameObject(null);
		}

		private void Update()
		{
			if (eventSystem.playerRoot != null)
				if (eventSystem.currentSelectedGameObject == null)
					eventSystem.SetSelectedGameObject(eventSystem.firstSelectedGameObject);
		}

		public void SetUIFocus(GameObject root, GameObject firstSelected)
		{
			eventSystem.playerRoot = root;
			eventSystem.firstSelectedGameObject = firstSelected;
			eventSystem.SetSelectedGameObject(firstSelected);
		}
	}
}