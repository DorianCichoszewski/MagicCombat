using MagicCombat.Shared.Data;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

namespace MagicCombat.Player
{
	public class PlayerInputController : MonoBehaviour
	{
		[SerializeField]
		private PlayerInput input;

		[SerializeField]
		private PlayerGameplayInput playerGameplayInput;

		[SerializeField]
		private MultiplayerEventSystem eventSystem;
		
		private PlayerData data;
		
		public PlayerData Data => data;

		public int Index => input.playerIndex;

		public PlayerData Init(StaticPlayerData staticData)
		{
			data = new PlayerData()
			{
				staticData = staticData,
				gameplayInput = playerGameplayInput,
				playerInputController = this,
				id = Index
			};
			return data;
		}

		public void Reset()
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