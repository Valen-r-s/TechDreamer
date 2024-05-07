using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]

public class MeshSubdivider : MonoBehaviour
{
    private Mesh mesh;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SubdivideMesh();
        }
    }

    void SubdivideMesh()
    {
        Vector3[] oldVertices = mesh.vertices;
        int[] oldTriangles = mesh.triangles;
        Dictionary<KeyValuePair<int, int>, int> newVerticesMap = new Dictionary<KeyValuePair<int, int>, int>();
        List<Vector3> newVertices = new List<Vector3>(oldVertices);
        List<int> newTriangles = new List<int>();

        for (int i = 0; i < oldTriangles.Length; i += 3)
        {
            int currentTriangleIndex1 = oldTriangles[i];
            int currentTriangleIndex2 = oldTriangles[i + 1];
            int currentTriangleIndex3 = oldTriangles[i + 2];

            int a = GetNewVertex(currentTriangleIndex1, currentTriangleIndex2, newVertices, newVerticesMap);
            int b = GetNewVertex(currentTriangleIndex2, currentTriangleIndex3, newVertices, newVerticesMap);
            int c = GetNewVertex(currentTriangleIndex3, currentTriangleIndex1, newVertices, newVerticesMap);

            // Adding four new triangles
            newTriangles.Add(currentTriangleIndex1);
            newTriangles.Add(a);
            newTriangles.Add(c);

            newTriangles.Add(currentTriangleIndex2);
            newTriangles.Add(b);
            newTriangles.Add(a);

            newTriangles.Add(currentTriangleIndex3);
            newTriangles.Add(c);
            newTriangles.Add(b);

            newTriangles.Add(a);
            newTriangles.Add(b);
            newTriangles.Add(c);
        }

        mesh.vertices = newVertices.ToArray();
        mesh.triangles = newTriangles.ToArray();
        mesh.RecalculateNormals();
    }

    int GetNewVertex(int index1, int index2, List<Vector3> newVertices, Dictionary<KeyValuePair<int, int>, int> newVerticesMap)
    {
        KeyValuePair<int, int> edgeKey = new KeyValuePair<int, int>(Mathf.Min(index1, index2), Mathf.Max(index1, index2));
        if (newVerticesMap.TryGetValue(edgeKey, out int existingVertex))
        {
            return existingVertex;
        }
        else
        {
            Vector3 newVertex = (newVertices[index1] + newVertices[index2]) / 2f;
            newVertices.Add(newVertex);
            int newIndex = newVertices.Count - 1;
            newVerticesMap.Add(edgeKey, newIndex);
            return newIndex;
        }
    }
}
