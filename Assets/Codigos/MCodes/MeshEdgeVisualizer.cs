using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter))]
public class MeshEdgeVisualizer : MonoBehaviour
{
    public Material lineMaterial; // Asigna esto desde el Inspector

    private Mesh mesh;
    private Vector3[] lastVertices;
    private int[] lastTriangles;
    private Dictionary<(int, int), GameObject> edgeVisuals = new Dictionary<(int, int), GameObject>(); // Para mantener referencias a los objetos de línea

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        lastVertices = mesh.vertices;
        lastTriangles = mesh.triangles;
        UpdateEdgeVisuals();
    }

    void Update()
    {
        if (HasMeshChanged())
        {
            UpdateEdgeVisuals();
            lastVertices = mesh.vertices;
            lastTriangles = mesh.triangles;
        }
    }

    private bool HasMeshChanged()
    {
        if (lastVertices.Length != mesh.vertices.Length || lastTriangles.Length != mesh.triangles.Length)
            return true;

        for (int i = 0; i < lastVertices.Length; i++)
            if (lastVertices[i] != mesh.vertices[i])
                return true;

        for (int i = 0; i < lastTriangles.Length; i++)
            if (lastTriangles[i] != mesh.triangles[i])
                return true;

        return false;
    }

    void UpdateEdgeVisuals()
    {
        ClearEdgeVisuals(); // Limpiar todas las visualizaciones antiguas primero
        var vertices = mesh.vertices;
        var triangles = mesh.triangles;

        // Identificar todas las aristas nuevas o existentes
        for (int i = 0; i < triangles.Length; i += 3)
        {
            var edge1 = (Mathf.Min(triangles[i], triangles[i + 1]), Mathf.Max(triangles[i], triangles[i + 1]));
            var edge2 = (Mathf.Min(triangles[i + 1], triangles[i + 2]), Mathf.Max(triangles[i + 1], triangles[i + 2]));
            var edge3 = (Mathf.Min(triangles[i + 2], triangles[i]), Mathf.Max(triangles[i + 2], triangles[i]));

            CreateOrUpdateLine(edge1, vertices[edge1.Item1], vertices[edge1.Item2]);
            CreateOrUpdateLine(edge2, vertices[edge2.Item1], vertices[edge2.Item2]);
            CreateOrUpdateLine(edge3, vertices[edge3.Item1], vertices[edge3.Item2]);
        }
    }

    void ClearEdgeVisuals()
    {
        foreach (var line in edgeVisuals.Values)
            Destroy(line);
        edgeVisuals.Clear();
    }

    void CreateOrUpdateLine((int, int) edge, Vector3 start, Vector3 end)
    {
        if (!edgeVisuals.ContainsKey(edge))
        {
            GameObject lineObj = new GameObject("Edge");
            LineRenderer lineRenderer = lineObj.AddComponent<LineRenderer>();
            lineRenderer.material = lineMaterial;
            lineRenderer.startWidth = 0.05f;
            lineRenderer.endWidth = 0.05f;
            lineRenderer.positionCount = 2;
            edgeVisuals[edge] = lineObj;
        }

        LineRenderer lr = edgeVisuals[edge].GetComponent<LineRenderer>();
        lr.SetPosition(0, transform.TransformPoint(start));
        lr.SetPosition(1, transform.TransformPoint(end));
    }
}
