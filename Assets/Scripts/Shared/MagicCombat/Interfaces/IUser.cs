using Shared.Data;
using UnityEngine;

namespace Shared.Interfaces
{
	public interface IUser
	{
		public string InputName { get; }
		public UserId Id { get; }

		public GameplayInputMapping GameplayInputMapping { get; }

		public void SetUIFocus(GameObject root, GameObject firstSelected, GameObject readyToggle = null);

		public void Destroy();
	}
}