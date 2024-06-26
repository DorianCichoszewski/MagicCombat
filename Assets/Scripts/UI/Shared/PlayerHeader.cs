using Shared.Data;
using TMPro;
using UnityEngine;

namespace MagicCombat.UI.Shared
{
	public class PlayerHeader : MonoBehaviour
	{
		[SerializeField]
		private TMP_Text text;

		public void Init(StaticUserData data)
		{
			text.text = data.name;
			text.color = data.color;
		}
	}
}