using Gameplay.Player.Ability;
using Gameplay.Player.Basic;
using UnityEngine;

namespace Gameplay.Player
{
	public class PlayerBase : MonoBehaviour
	{
		[SerializeField]
		private MovementController movement;

		[SerializeField]
		private SkinController skin;

		[HideInInspector]
		public StartData.PlayerInit data;

		[Space]
		public AbilityCaster utility;
		public AbilityCaster skill1;
		public AbilityCaster skill2;
		public AbilityCaster skill3;

		private GameplayManager gameplayManager;

		public MovementController MovementController => movement;

		public GameplayGlobals GameplayGlobals => gameplayManager.GameplayGlobals;

		public void Init(StartData.PlayerInit initData, GameplayManager manager)
		{
			gameplayManager = manager;
			data = initData;
			skin.SetSkin(data);
			transform.position = data.spawnPos;
			movement.Init(manager.GameplayGlobals);
			
			utility.Init(this);
			skill1.Init(this);
			skill2.Init(this);
			skill3.Init(this);
		}

		public void Hit()
		{
			gameplayManager.OnPlayerDeath(this);
		}
	}
}