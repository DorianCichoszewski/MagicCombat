using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MagicCombat.UI.Shared
{
	public class ReadyToggle : Toggle
	{
		public override void OnDeselect(BaseEventData eventData)
		{
			base.OnDeselect(eventData);
			isOn = false;
		}
	}
}