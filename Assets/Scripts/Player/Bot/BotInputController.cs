using Shared.Data;
using Shared.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace MagicCombat.Player.Bot
{
	public class BotInputController : MonoBehaviour, IPlayerInputController
	{
		[SerializeField]
		private PlayerInput input;
		
		[SerializeField]
		private BotGameplayInput botGameplayInput;

		public IGameplayInputController GameplayInputController => botGameplayInput;
		public string InputName => "Bot";
		
		public PlayerId Id { get; private set; }
		
		public void SetId(PlayerId playerId)
		{
			Id = playerId;
			playerId.ChangeControllerStatus(true);
		}
		
		public void SetUIFocus(GameObject root, GameObject firstSelected, GameObject readyToggle = null)
		{
			if (readyToggle != null)
			{
				readyToggle.GetComponent<Toggle>().isOn = true;
			}
		}

		public void Destroy()
		{
			Destroy(gameObject);
		}
	}
}