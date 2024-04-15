using UnityEngine;

namespace MagicCombat.Shared.Interfaces
{
	public interface IPlayerInputController
	{
		public IGameplayInputController GameplayInputController { get; }

		public string InputName { get; }

		public void SetUIFocus(GameObject root, GameObject firstSelected);
	}
}