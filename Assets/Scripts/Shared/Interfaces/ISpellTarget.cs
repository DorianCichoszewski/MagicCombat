using UnityEngine;

namespace Shared.Interfaces
{
	public interface ISpellTarget
	{
		public void Kill();

		public void AddForce(Vector2 force, float forceDuration = 0f);

		public Vector2 Position { get; }
	}
}