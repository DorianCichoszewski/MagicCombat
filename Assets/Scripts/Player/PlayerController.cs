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
		
		private GameplayManager gameplayManager;
		private PlayerAvatar avatar;

		public PlayerInput Input => input;
		public MultiplayerEventSystem EventSystem => eventSystem;
		public StartData.PlayerInit Data => startData.playerInitList[Index];
		public PlayerAvatar Avatar => avatar;
		public int Index => input.playerIndex;

		public void Init()
		{
			gameObject.name = Data.name;
		}

		public void Reset()
		{
			eventSystem.playerRoot = null;
			eventSystem.SetSelectedGameObject(null);
		}
		
		public bool EnableInput
		{
			get => enableInput && avatar != null;
			set => enableInput = value;
		}

		public void StartGame()
		{
			avatar = Instantiate(avatarPrefab, Data.spawnPos, Quaternion.identity);
			avatar.Init(this, gameplayManager);
		}
	}
}