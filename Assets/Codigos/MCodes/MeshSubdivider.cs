using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshSubdivider : MonoBehaviour
{
    private Mesh mesh;
    private EdgeSelector edgeSelector;
    private VertexSelector vertexSelector;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        edgeSelector = GetComponent<EdgeSelector>();
        vertexSelector = GetComponent<VertexSelector>();
    }

    public void SubdivideMesh()
    {
        Vector3[] oldVertices = mesh.vertices;
        int[] oldTriangles = mesh.triangles;
        Dictionary<Edge, int> newVerticesMap = new Dictionary<Edge, int>();
        List<Vector3> newVertices = new List<Vector3>(oldVertices);
        List<int> newTriangles = new List<int>();

        // Asignar nuevos vértices para cada arista solo una vez
        for (int i = 0; i < oldTriangles.Length; i += 3)
        {
            int v0 = oldTriangles[i];
            int v1 = oldTriangles[i + 1];
            int v2 = oldTriangles[i + 2];

            int a = GetNewVertex(v0, v1, newVertices, newVerticesMap);
            int b = GetNewVertex(v1, v2, newVertices, newVerticesMap);
            int c = GetNewVertex(v2, v0, newVertices, newVerticesMap);

            newTriangles.Add(v0); newTriangles.Add(a); newTriangles.Add(c);
            newTriangles.Add(v1); newTriangles.Add(b); newTriangles.Add(a);
            newTriangles.Add(v2); newTriangles.Add(c); newTriangles.Add(b);
            newTriangles.Add(a); newTriangles.Add(b); newTriangles.Add(c);
        }

        mesh.Clear(); // Limpiar el mesh antes de asignar nuevos valores
        mesh.vertices = newVertices.ToArray();
        mesh.triangles = newTriangles.ToArray();
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        UpdateMeshCollider();

        // Asegurarse de que los selectores se actualicen con la nueva malla
        edgeSelector.ReloadMeshData();
        vertexSelector.UpdateVertexPositions();
    }

    private void UpdateMeshCollider()
    {
        MeshCollider meshCollider = GetComponent<MeshCollider>();
        if (meshCollider)
        {
            meshCollider.sharedMesh = null;
            meshCollider.sharedMesh = mesh;
        }
    }

    private int GetNewVertex(int index1, int index2, List<Vector3> newVertices, Dictionary<Edge, int> newVerticesMap)
    {
        Edge edge = new Edge(index1, index2);
        if (newVerticesMap.TryGetValue(edge, out int newIndex))
        {
            return newIndex;
        }
        else
        {
            Vector3 newVertex = (newVertices[index1] + newVertices[index2]) / 2.0f;
            newVertices.Add(newVertex);
            newIndex = newVertices.Count - 1;
            newVerticesMap.Add(edge, newIndex);
            return newIndex;
        }
    }

    private struct Edge
    {
        public int Vertex1;
        public int Vertex2;

        public Edge(int vertex1, int vertex2)
        {
            Vertex1 = Mathf.Min(vertex1, vertex2);
            Vertex2 = Mathf.Max(vertex1, vertex2);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Vertex1.GetHashCode();
                hash = hash * 23 + Vertex2.GetHashCode();
                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Edge)) return false;
            Edge edge = (Edge)obj;
            return Vertex1 == edge.Vertex1 && Vertex2 == edge.Vertex2;
        }
    }
}
