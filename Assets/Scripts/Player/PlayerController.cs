using Gameplay;
using Gameplay.Player;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

namespace Player
{
	[RequireComponent(typeof(PlayerInput), typeof(PlayerInputMapper))]
	public class PlayerController : MonoBehaviour
	{
		[SerializeField]
		private StartData startData;
		[SerializeField]
		private PlayerAvatar avatarPrefab;
		[SerializeField]
		private PlayerInput input;
		[SerializeField]
		private MultiplayerEventSystem eventSystem;
		
		[SerializeField]
		private bool enableInput = true;
		
		private PlayerAvatar avatar;

		public PlayerInput Input => input;
		public MultiplayerEventSystem EventSystem => eventSystem;
		public StartData.PlayerInit InitData => startData.playerInitList[Index];
		public StartData StartData => startData;
		public PlayerAvatar Avatar => avatar;
		public int Index => input.playerIndex;

		public void Init()
		{
			gameObject.name = InitData.name;
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
			{
				if (eventSystem.currentSelectedGameObject == null)
					eventSystem.SetSelectedGameObject(eventSystem.firstSelectedGameObject);
			}
		}

		public bool EnableInput
		{
			get => enableInput && avatar != null;
			set => enableInput = value;
		}

		public void CreateAvatar(GameplayManager manager, PlayerData data)
		{
			avatar = Instantiate(avatarPrefab, InitData.spawnPos, Quaternion.identity);
			avatar.Init(data, manager);
			enableInput = true;
		}
	}
}