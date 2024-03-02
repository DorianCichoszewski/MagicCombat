using TMPro;
using UnityEngine;

namespace UI
{
	public class PlayerHeader : MonoBehaviour
	{
		[SerializeField]
		private TMP_Text text;

		public void Init(StartData.PlayerInit data)
		{
			text.text = data.name;
			text.color = data.color;
		}
	}
}