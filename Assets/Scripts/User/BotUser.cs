using Shared.Data;
using Shared.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace MagicCombat.User
{
	internal class BotUser : IUser
	{
		public string InputName => $"Bot {(int)Id}";
		public UserId Id { get; }
		public GameplayInputMapping GameplayInputMapping { get; }

		public BotUser(UserId id)
		{
			Id = id;
			GameplayInputMapping = new GameplayInputMapping();
		}

		internal BotUser(IUser previousUser)
		{
			Id = previousUser.Id;
			GameplayInputMapping = previousUser.GameplayInputMapping;
		}

		public void SetUIFocus(GameObject root, GameObject firstSelected, GameObject readyToggle = null)
		{
			if (readyToggle != null && readyToggle.TryGetComponent<Toggle>(out var readyToggleComponent))
				readyToggleComponent.isOn = true;
		}

		public void Destroy() { }
	}
}