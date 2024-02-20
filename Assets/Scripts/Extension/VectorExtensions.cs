using UnityEngine;

namespace Extension
{
	public static class VectorExtensions
	{
		public static Vector3 ToVec3(this Vector2 vec)
		{
			return new Vector3(vec.x, 0, vec.y);
		}

		public static Vector3 ToVec3(this Vector2 vec, float height)
		{
			return new Vector3(vec.x, height, vec.y);
		}

		public static Vector2 ToVec2(this Vector3 vec)
		{
			return new Vector2(vec.x, vec.z);
		}
	}
}