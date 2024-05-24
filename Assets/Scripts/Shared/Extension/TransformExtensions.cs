using UnityEngine;

namespace Shared.Extension
{
	public static class TransformExtensions
	{
		public static void Translate2(this Transform transform, Vector2 vector2, Space relativeTo = Space.World)
		{
			transform.Translate(vector2.ToVec3(), relativeTo);
		}
	}
}