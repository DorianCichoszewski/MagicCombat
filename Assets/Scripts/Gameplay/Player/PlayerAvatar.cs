using MagicCombat.Gameplay.Abilities;
using MagicCombat.Gameplay.Abilities.Base;
using MagicCombat.Gameplay.Avatar;
using MagicCombat.Gameplay.Avatar.Movement;
using MagicCombat.Gameplay.Notifications;
using Shared.Data;
using Shared.Interfaces;
using Shared.Services;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MagicCombat.Gameplay.Player
{
	public class PlayerAvatar : MonoBehaviour
	{
		[SerializeField]
		private EventChannelPlayerAvatar playerHitChannel;

		[SerializeField]
		private BaseAvatar avatar;

		[ReadOnly]
		[ShowInInspector]
		public UserId Id { get; private set; }

		[ShowInInspector]
		public AbilityCaster skill1;

		[ShowInInspector]
		public AbilityCaster skill2;

		[ShowInInspector]
		public AbilityCaster skill3;

		[ShowInInspector]
		public AbilityCaster utility;

		public PlayerController Controller { get; private set; }

		public MovementController MovementController => avatar.MovementController;
		public Vector2 Position => avatar.Position;

		public bool Alive => avatar.Alive;

		public void Init(AbilityPlayerData abilityPlayerData, StaticUserData staticData,
			GameplayInputMapping input, UserId id)
		{
			Id = id;

			var abilitiesContext = ScriptableLocator.Get<AbilitiesContext>();
			avatar.Init(staticData, abilitiesContext.PhysicClock);
			avatar.OnDeath += OnAvatarDeath;

			utility = new AbilityCaster(avatar, abilitiesContext.AbilitiesCollection[abilityPlayerData.UtilityKey],
				abilitiesContext);
			skill1 = new AbilityCaster(avatar, abilitiesContext.AbilitiesCollection[abilityPlayerData.Skill1Key],
				abilitiesContext);
			skill2 = new AbilityCaster(avatar, abilitiesContext.AbilitiesCollection[abilityPlayerData.Skill2Key],
				abilitiesContext);
			skill3 = new AbilityCaster(avatar, abilitiesContext.AbilitiesCollection[abilityPlayerData.Skill3Key],
				abilitiesContext);

			Controller = new PlayerController(this, input)
			{
				EnabledInput = true
			};
		}

		private void OnAvatarDeath()
		{
			Controller.Clear();
			Controller = null;
			playerHitChannel.Invoke(this);
		}
	}
}