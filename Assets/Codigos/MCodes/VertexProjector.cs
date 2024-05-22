using System.Collections.Generic;
using UnityEngine;

public class VertexProjector : MonoBehaviour
{
    public Camera frontCamera;
    public Camera sideCamera;
    public Transform frontPlane;
    public Transform sidePlane;

    private Mesh modelMesh;

    void Start()
    {
        // Obtener el Mesh del MeshFilter del cubo
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter != null)
        {
            modelMesh = meshFilter.mesh;
        }
        else
        {
            Debug.LogError("No MeshFilter found on the GameObject.");
        }
    }

    public List<Vector2> ProjectVertices(Camera camera, Transform plane)
    {
        Vector3[] vertices = modelMesh.vertices;
        List<Vector2> projectedVertices = new List<Vector2>();

        foreach (var vertex in vertices)
        {
            Vector3 worldPoint = transform.TransformPoint(vertex);
            Ray ray = camera.ScreenPointToRay(camera.WorldToScreenPoint(worldPoint));

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                if (hit.transform == plane)
                {
                    Vector2 localPoint = hit.textureCoord;
                    projectedVertices.Add(localPoint);
                }
            }
        }

        return projectedVertices;
    }
}
