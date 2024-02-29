using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Player
{
	[RequireComponent(typeof(PlayerInput), typeof(PlayerInputMapper))]
	public class PlayerController : MonoBehaviour
	{
		[SerializeField]
		private PlayerAvatar avatarPrefab;
		[SerializeField]
		private PlayerInput input;
		[SerializeField]
		private bool enableInput = true;

		private StartData.PlayerInit data;
		private GameplayManager gameplayManager;
		private PlayerAvatar avatar;
		private int index;

		public PlayerInput Input => input;
		public StartData.PlayerInit Data => data;
		public PlayerAvatar Avatar => avatar;
		public int Index => index;
		
		public bool EnableInput
		{
			get => enableInput && avatar != null;
			set => enableInput = value;
		}

		public void Init(StartData.PlayerInit initData, GameplayManager manager, int index)
		{
			this.index = index;
			data = initData;
			gameplayManager = manager;
		}

		public void StartGame()
		{
			avatar = Instantiate(avatarPrefab, data.spawnPos, Quaternion.identity, transform);
			avatar.Init(this, gameplayManager);
		}
	}
}