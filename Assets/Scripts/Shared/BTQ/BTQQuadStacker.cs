using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MagicCombat
{
	public class BTQQuadStacker : MonoBehaviour, IMeshModifier
	{
		[SerializeField]
		private int quadsCount = 1;

		[SerializeField]
		private float quadDistance = 0.1f;

		private void OnValidate()
		{
			if (TryGetComponent(out Graphic graphic))
			{
				graphic.SetVerticesDirty();
			}
		}


		public void ModifyMesh(Mesh mesh)
		{
			if (mesh.vertexCount == 4)
			{
				var original = mesh.vertices;
				var vertices = new Vector3[quadsCount * 4];
				var colors = new Color[vertices.Length];
				for (int i = 0; i < quadsCount; i++)
				{
					vertices[i * 4] = new Vector3(original[0].x, original[0].y, original[0].z + i * quadDistance);
					vertices[i * 4 + 1] = new Vector3(original[1].x, original[1].y, original[1].z + i * quadDistance);
					vertices[i * 4 + 2] = new Vector3(original[2].x, original[2].y, original[2].z + i * quadDistance);
					vertices[i * 4 + 3] = new Vector3(original[3].x, original[3].y, original[3].z + i * quadDistance);

					float vertexTint = i / (float)(quadsCount - 1);
					var color = new Color(vertexTint, vertexTint, vertexTint, 1);

					colors[4 * i + 0] = color;
					colors[4 * i + 1] = color;
					colors[4 * i + 2] = color;
					colors[4 * i + 3] = color;
				}

				mesh.vertices = vertices;
				mesh.colors = colors;
				mesh.uv = QuadHelper.GetMultipleQuadUVs(quadsCount);
				mesh.normals = QuadHelper.GetMultipleQuadNormals(quadsCount);
				mesh.triangles = QuadHelper.GetMultipleQuadTriangles(quadsCount);
			}
			else
			{
				Debug.LogError($"Quad Stacker only accept quads as input", this);
			}
		}

		public void ModifyMesh(VertexHelper verts)
		{
			var uv = QuadHelper.GetQuadUVs();

			if (verts.currentVertCount == 4)
			{
				List<UIVertex> original = new();
				verts.GetUIVertexStream(original);
				// verts are in triangles order, but swapping one gives in vertices order
				original[3] = original[4];
				
				verts.Clear();

				for (int i = 0; i < quadsCount; i++)
				{
					float vertexTint = i / (float)(quadsCount - 1);
					var color = new Color(vertexTint, vertexTint, vertexTint, 1);

					var quadVerts = new UIVertex[4];

					for (int j = 0; j < 4; j++)
					{
						var position = original[j].position;
						position.z += i * quadDistance;
						quadVerts[j] = new UIVertex()
						{
							position = position,
							color = color,
							uv1 = uv[j],
							uv2 = uv[j],
							normal = original[j].normal,
							tangent = original[j].tangent
						};
					}
					verts.AddUIVertexQuad(quadVerts);
				}
			}
			else
			{
				Debug.LogError($"Quad Stacker only accept quads as input", this);
			}
		}
	}
}