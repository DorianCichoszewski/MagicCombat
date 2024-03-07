using MagicCombat.Gameplay;
using MagicCombat.Gameplay.Player;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

namespace MagicCombat.Player
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

		public PlayerInput Input => input;
		public MultiplayerEventSystem EventSystem => eventSystem;
		public StartData.PlayerInit InitData => startData.playerInitList[Index];
		public StartData StartData => startData;
		public PlayerAvatar Avatar { get; private set; }

		public int Index => input.playerIndex;

		public bool EnableInput
		{
			get => enableInput && Avatar != null;
			set => enableInput = value;
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

		public void Init()
		{
			gameObject.name = InitData.name;
		}

		public void CreateAvatar(GameplayManager manager, PlayerData data)
		{
			Avatar = Instantiate(avatarPrefab, InitData.spawnPos, Quaternion.identity);
			Avatar.Init(data, manager, manager.GameplayGlobals);
			enableInput = true;
		}
	}
}