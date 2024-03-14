using UnityEngine;

namespace MagicCombat.Extension
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

		public static Quaternion ToRotation(this Vector2 vec)
		{
			return Quaternion.Euler(0, vec.ToAngleRotation(), 0);
		}
		
		public static float ToAngleRotation(this Vector2 vec)
		{
			var normalized = vec.normalized;
			return Mathf.Rad2Deg * Mathf.Atan2(normalized.x, normalized.y);
		}
	}
}