using Shared.Data;
using UnityEngine;

namespace MagicCombat.Gameplay.Avatar
{
	public class SkinController : MonoBehaviour
	{
		[SerializeField]
		private MeshRenderer[] renderers;

		public void SetSkin(StaticUserData data)
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