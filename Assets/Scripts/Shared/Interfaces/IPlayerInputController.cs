using MagicCombat.Shared.Data;
using UnityEngine;

namespace MagicCombat.Shared.Interfaces
{
	public interface IPlayerInputController
	{
		public IGameplayInputController GameplayInputController { get; }

		public string InputName { get; }
		
		public PlayerId Id { get; }

		public void SetId(PlayerId playerId);

		public void SetUIFocus(GameObject root, GameObject firstSelected, GameObject readyToggle = null);

		public void Destroy();
	}
}