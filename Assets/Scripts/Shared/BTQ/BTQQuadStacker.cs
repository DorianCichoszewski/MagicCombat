using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Shared.BTQ
{
	[RequireComponent(typeof(RectTransform))]
	[ExecuteAlways]
	public class BTQQuadStacker : MonoBehaviour, IMeshModifier
	{
		[SerializeField]
		private int quadsCount = 1;

		[SerializeField]
		private Vector2 quadSize = new (10, 10);

		[SerializeField]
		private float quadZDistance = 0.1f;

		private Canvas canvas;
		private Graphic graphic;
		private RectTransform rectTransform;
		private static readonly int EmitterDimensions = Shader.PropertyToID("_EmitterDimensions");

		private Vector2 LastEmitterDimensions;

		private void OnValidate()
		{
			rectTransform ??= GetComponent<RectTransform>();
			canvas ??= GetComponentInParent<Canvas>(false);
			graphic ??= GetComponent<Graphic>();
			EnsureAdditionalChannels();
			graphic.SetVerticesDirty();
		}

		private void Update()
		{
			Vector2 size = graphic.material.GetVector(EmitterDimensions);
			if (size == LastEmitterDimensions) return;
			
			LastEmitterDimensions = size;
			size = 2 * LastEmitterDimensions + quadSize;
			rectTransform.sizeDelta = size;
		}

		private void EnsureAdditionalChannels()
		{
			canvas.additionalShaderChannels |= AdditionalCanvasShaderChannels.Tangent;
		}

		public void ModifyMesh(Mesh mesh)
		{
			if (mesh.vertexCount != 4)
			{
				Debug.LogError($"Quad Stacker only accept quads as input", this);
				return;
			}
			
			var halfSize = quadSize * 0.5f;
			var vertices = new Vector3[quadsCount * 4];
			var colors = new Color[vertices.Length];
			for (int i = 0; i < quadsCount; i++)
			{
				vertices[i * 4] = new Vector3(-halfSize.x, halfSize.y, i * quadZDistance);
				vertices[i * 4 + 1] = new Vector3(halfSize.x, halfSize.y, i * quadZDistance);
				vertices[i * 4 + 2] = new Vector3(-halfSize.x, -halfSize.y, i * quadZDistance);
				vertices[i * 4 + 3] = new Vector3(halfSize.x, -halfSize.y, i * quadZDistance);

				float vertexTint = i / (float)quadsCount;
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

		public void ModifyMesh(VertexHelper verts)
		{
			if (verts.currentVertCount != 4)
			{
				Debug.LogError($"Quad Stacker only accept quads as input", this);
				return;
			}

			Vector3 localPosition = transform.localPosition;
			var halfSize = quadSize * 0.5f;

			List<UIVertex> original = new();
			verts.GetUIVertexStream(original);

			// verts are in triangles order, but swapping one gives in vertices order
			original[3] = original[4];
			verts.Clear();

			var quadVerts = new UIVertex[4];
			var vertices = new Vector3[4];

			for (int i = 0; i < quadsCount; i++)
			{
				float vertexTint = i / (float)quadsCount;
				
				vertices[0] = new Vector3(-halfSize.x, -halfSize.y, i * quadZDistance);
				vertices[1] = new Vector3(-halfSize.x, halfSize.y, i * quadZDistance);
				vertices[2] = new Vector3(halfSize.x, halfSize.y, i * quadZDistance);
				vertices[3] = new Vector3(halfSize.x, -halfSize.y, i * quadZDistance);

				for (int j = 0; j < 4; j++)
				{
					quadVerts[j] = new UIVertex
					{
						position = vertices[j],
						color = original[j].color,
						uv0 = original[j].uv0,
						uv1 = original[j].uv0,
						uv2 = original[j].uv0,
						uv3 = original[j].uv0,
						normal = original[j].normal,
						tangent = new Vector4(localPosition.x, localPosition.y, localPosition.z, vertexTint),
					};
				}

				verts.AddUIVertexQuad(quadVerts);
			}
		}
	}
}