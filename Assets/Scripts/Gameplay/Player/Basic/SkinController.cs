using UnityEngine;

namespace MagicCombat.Gameplay.Player.Basic
{
	public class SkinController : MonoBehaviour
	{
		[SerializeField]
		private MeshRenderer[] renderers;

		public void SetSkin(StartData.PlayerInit data)
		{
			foreach (var renderer in renderers)
			{
				renderer.material = data.material;
			}
		}

		[ContextMenu(nameof(GetMeshRenderers))]
		private void GetMeshRenderers()
		{
			renderers = GetComponentsInChildren<MeshRenderer>();
		}
	}
}