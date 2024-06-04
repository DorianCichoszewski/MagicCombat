using Sirenix.OdinInspector;
using UnityEngine;

namespace MagicCombat
{
    [RequireComponent(typeof(CanvasRenderer))]
    public class MeshStack : MonoBehaviour
    {
        [SerializeField]
        private Vector2 size = new(1,1);
        
        [SerializeField]
        private int count = 1;

        [SerializeField]
        private float distance = 0.5f;

        [PreviewField(Height = 200)]
        [ShowInInspector]
        private Mesh mesh;
        
        [Button]
        public void Test()
        {
            var rectTransform = GetComponent<RectTransform>();
            mesh = new Mesh();

            size = rectTransform.sizeDelta;

            var pivot = rectTransform.pivot;

            Vector3[] vertices = new Vector3[4 * count];
            Vector3[] normals = new Vector3[vertices.Length];
            Vector2[] uv = new Vector2[vertices.Length];
            Color[] colors = new Color[vertices.Length];
            int[] tris = new int[6 * count];

            float left = -pivot.x * size.x;
            float right = (1 - pivot.x) * size.x;
            float bottom = -pivot.y * size.y;
            float top = (1 - pivot.y) * size.y;
            

            for (int i = 0; i < count; i++)
            {
                vertices[4 * i + 0] = new Vector3(left, bottom, i * distance);
                vertices[4 * i + 1] = new Vector3(right, bottom, i * distance);
                vertices[4 * i + 2] = new Vector3(left, top, i * distance);
                vertices[4 * i + 3] = new Vector3(right, top, i * distance);
                
                normals[4 * i + 0] = Vector3.forward;
                normals[4 * i + 1] = Vector3.forward;
                normals[4 * i + 2] = Vector3.forward;
                normals[4 * i + 3] = Vector3.forward;
                
                uv[4 * i + 0] = new Vector2(0, 0);
                uv[4 * i + 1] = new Vector2(1, 0);
                uv[4 * i + 2] = new Vector2(0, 1);
                uv[4 * i + 3] = new Vector2(1, 1);

                float vertexTint = i / (float)(count - 1);
                var color = new Color(vertexTint, vertexTint, vertexTint, 1);

                colors[4 * i + 0] = color;
                colors[4 * i + 1] = color;
                colors[4 * i + 2] = color;
                colors[4 * i + 3] = color;
                
                tris[6 * i + 0] = 4 * i + 0;
                tris[6 * i + 1] = 4 * i + 1;
                tris[6 * i + 2] = 4 * i + 2;
                tris[6 * i + 3] = 4 * i + 2;
                tris[6 * i + 4] = 4 * i + 1;
                tris[6 * i + 5] = 4 * i + 3;
            }
            
            mesh.vertices = vertices;
            mesh.normals = normals;
            mesh.uv = uv;
            mesh.triangles = tris;
            mesh.colors = colors;
            
            GetComponent<CanvasRenderer>().SetMesh(mesh);
        }
    }
}
