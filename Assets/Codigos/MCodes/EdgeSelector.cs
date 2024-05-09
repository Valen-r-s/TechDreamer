using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshCollider))]
public class EdgeSelector : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject edgeMarkerPrefab; // Asigna tu prefab de cilindro en el inspector
    public Color preselectColor = Color.yellow; // Color cuando el mouse pasa encima
    public Color selectColor = Color.green; // Color al hacer clic en la arista
    public float sensitivity;

    private VertexSelector vertexSelector;
    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;
    private GameObject currentEdgeMarker; // Para mantener el cilindro actual
    private Edge? selectedEdge = null; // La arista actualmente seleccionada
    private Vector3 lastMousePosition;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        triangles = mesh.triangles;
        vertexSelector = GetComponent<VertexSelector>();
    }

    void Update()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices; // Asegúrate de que esta línea esté al principio del método Update.

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                HashSet<Edge> uniqueEdges = CalculateUniqueEdges();
                Edge? closestEdge = FindClosestEdgeToRay(hit.point, uniqueEdges);

                if (closestEdge.HasValue)
                {
                    if (currentEdgeMarker == null || !selectedEdge.Equals(closestEdge))
                    {
                        UpdateEdgeMarker(closestEdge.Value, preselectColor);
                    }

                    if (Input.GetMouseButtonDown(0)) // Al hacer clic, cambiar el color
                    {
                        selectedEdge = closestEdge;
                        UpdateEdgeMarker(closestEdge.Value, selectColor);
                    }
                }
            }
        }
        else
        {
            if (currentEdgeMarker != null) Destroy(currentEdgeMarker);
        }

        if (Input.GetMouseButtonUp(0))
        {
            selectedEdge = null; // Deseleccionar la arista al soltar el botón
        }
        if (selectedEdge.HasValue && Input.GetMouseButton(0))
        {
            MoveSelectedEdge();
        }

        lastMousePosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.nearClipPlane));
    
    }


    HashSet<Edge> CalculateUniqueEdges()
    {
        HashSet<Edge> edges = new HashSet<Edge>();
        for (int i = 0; i < triangles.Length; i += 3)
        {
            edges.Add(new Edge(triangles[i], triangles[i + 1]));
            edges.Add(new Edge(triangles[i + 1], triangles[i + 2]));
            edges.Add(new Edge(triangles[i + 2], triangles[i]));
        }
        return edges;
    }

    Edge? FindClosestEdgeToRay(Vector3 hitPoint, HashSet<Edge> edges)
    {
        Edge? closestEdge = null;
        float closestDistance = Mathf.Infinity;
        foreach (Edge edge in edges)
        {
            Vector3 point1 = transform.TransformPoint(vertices[edge.v1]);
            Vector3 point2 = transform.TransformPoint(vertices[edge.v2]);
            Vector3 closestPoint = ClosestPointOnLineSegment(point1, point2, hitPoint);
            float distance = Vector3.Distance(hitPoint, closestPoint);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEdge = edge;
            }
        }
        return closestEdge;
    }

    Vector3 ClosestPointOnLineSegment(Vector3 p1, Vector3 p2, Vector3 p)
    {
        Vector3 lineDirection = p2 - p1;
        float lineLength = lineDirection.magnitude;
        lineDirection.Normalize();
        float projectLength = Mathf.Clamp(Vector3.Dot(p - p1, lineDirection), 0f, lineLength);
        return p1 + lineDirection * projectLength;
    }
    
    private void MoveSelectedEdge()
    {
        Vector3 mouseDelta = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * sensitivity;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(mainCamera.transform.forward, transform.position);

        if (plane.Raycast(ray, out float enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            Vector3 movement = mainCamera.transform.TransformDirection(mouseDelta) * (hitPoint - mainCamera.transform.position).magnitude * 0.1f;

            if (selectedEdge.HasValue)
            {
                Edge edge = selectedEdge.Value;
                ApplyMovementToEdge(edge, movement);
                // Actualizar el marcador de arista después de mover la arista
                UpdateEdgeMarker(edge, selectColor);
            }

            lastMousePosition = Input.mousePosition;
            vertexSelector.UpdateVertexPositions();
        }
    }

    private void ApplyMovementToEdge(Edge edge, Vector3 movement)
    {
        Vector3 worldMovement = transform.TransformVector(movement);
        Vector3 originalPositionV1 = vertices[edge.v1];
        Vector3 originalPositionV2 = vertices[edge.v2];

        for (int i = 0; i < vertices.Length; i++)
        {
            if (vertices[i] == originalPositionV1 || vertices[i] == originalPositionV2)
            {
                vertices[i] += transform.InverseTransformDirection(worldMovement);
            }
        }

        mesh.vertices = vertices;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        UpdateMeshCollider();
    }

    void UpdateMeshCollider()
    {
        MeshCollider meshCollider = GetComponent<MeshCollider>();
        meshCollider.sharedMesh = null;
        meshCollider.sharedMesh = mesh;
    }

    void UpdateEdgeMarker(Edge edge, Color color)
    {
        Vector3 startPos = transform.TransformPoint(vertices[edge.v1]);
        Vector3 endPos = transform.TransformPoint(vertices[edge.v2]);

        if (currentEdgeMarker != null) Destroy(currentEdgeMarker);

        currentEdgeMarker = Instantiate(edgeMarkerPrefab, (startPos + endPos) / 2, Quaternion.LookRotation(endPos - startPos));
        currentEdgeMarker.transform.up = (endPos - startPos).normalized;
        float length = Vector3.Distance(startPos, endPos);
        currentEdgeMarker.transform.localScale = new Vector3(currentEdgeMarker.transform.localScale.x, length / 2, currentEdgeMarker.transform.localScale.z);
        currentEdgeMarker.GetComponent<Renderer>().material.color = color;
    }


    public void ReloadMeshData()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        triangles = mesh.triangles;
        // Asimismo, recalcular las aristas únicas para reflejar la malla actualizada
        CalculateUniqueEdges();
    }


}
