using System;
using MagicCombat.Shared.Data;
using MagicCombat.Shared.GameState;
using MagicCombat.Shared.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace MagicCombat.UI.Shared
{
	public class PerPlayerWindow : MonoBehaviour
	{
		[SerializeField]
		[Required]
		private Selectable firstElement;

		[SerializeField]
		private ReadyToggle readyToggle;

		protected SharedScriptable sharedScriptable;
		protected PlayerId playerId;

		private Action onReady;

		public bool IsReady => readyToggle?.isOn ?? true;
		protected IPlayerProvider PlayerProvider => sharedScriptable.PlayerProvider;

		public void Init(SharedScriptable shared, PlayerId id, Action onReady)
		{
			sharedScriptable = shared;
			playerId = id;
			this.onReady = onReady;

			var inputController = PlayerProvider.InputController(id);
			inputController.SetUIFocus(gameObject, firstElement.gameObject);

			readyToggle?.onValueChanged.AddListener(PressedReady);
		}

		protected virtual void OnInit() { }

		protected virtual bool CanBeReady()
		{
			return true;
		}

		private void PressedReady(bool newValue)
		{
			if (!newValue) return;

			if (!CanBeReady())
			{
				readyToggle.SetIsOnWithoutNotify(false);
				return;
			}

			onReady();
		}
	}
}