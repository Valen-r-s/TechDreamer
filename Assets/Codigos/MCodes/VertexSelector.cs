using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshCollider))]
public class VertexSelector : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject vertexMarkerPrefab; // Prefab para el marcador de vértice
    private GameObject preselectMarker; // Marcador para el vértice preseleccionado
    private GameObject selectMarker; // Marcador para el vértice seleccionado
    private Mesh mesh;
    private Vector3[] vertices;
    private bool vertexSelected = false;
    private int selectedVertexIndex = -1;
    private int preselectedVertexIndex = -1; // Índice del vértice preseleccionado
    private Vector3 initialMousePosition;
    private Vector3 initialVertexPosition;

    public Color preselectColor = Color.yellow; // Color para preselección
    public Color selectColor = Color.green; // Color para selección

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
    }

    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);
        float closestDistance = Mathf.Infinity;
        int closestVertexIndex = -1;
        bool hitDetected = false;

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject == gameObject)
            {
                Vector3 hitPoint = hit.point;
                int vertexIndex = FindClosestVertexIndex(hitPoint);
                float distance = Vector3.Distance(mainCamera.transform.position, hitPoint);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestVertexIndex = vertexIndex;
                    hitDetected = true;
                }
            }
        }

        if (hitDetected)
        {
            if (!vertexSelected && closestVertexIndex != preselectedVertexIndex)
            {
                UpdatePreselectMarker(transform.TransformPoint(vertices[closestVertexIndex]));
                preselectedVertexIndex = closestVertexIndex;
            }
        }
        else
        {
            RemovePreselectMarker();
            preselectedVertexIndex = -1;
        }

        if (Input.GetMouseButtonDown(0) && preselectedVertexIndex != -1)
        {
            vertexSelected = true;
            selectedVertexIndex = preselectedVertexIndex;
            initialMousePosition = Input.mousePosition;
            initialVertexPosition = transform.TransformPoint(vertices[selectedVertexIndex]);
            UpdateSelectMarker(initialVertexPosition);
        }

        if (vertexSelected && Input.GetMouseButton(0))
        {
            Vector3 currentMousePosition = Input.mousePosition;
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(new Vector3(currentMousePosition.x, currentMousePosition.y, Camera.main.WorldToScreenPoint(initialVertexPosition).z));
            MoveSelectedVertex(newPosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (vertexSelected)
            {
                RemoveSelectMarker(); // Asegúrese de eliminar el marcador de selección al finalizar el movimiento
                vertexSelected = false;
            }
        }
    }

    void UpdatePreselectMarker(Vector3 position)
    {
        if (preselectMarker == null)
        {
            preselectMarker = Instantiate(vertexMarkerPrefab, position, Quaternion.identity);
        }
        else
        {
            preselectMarker.transform.position = position;
        }
        preselectMarker.GetComponent<Renderer>().material.color = preselectColor;
    }

    void UpdateSelectMarker(Vector3 position)
    {
        // Eliminar el marcador de preselección si existe
        RemovePreselectMarker();

        if (selectMarker != null)
        {
            Destroy(selectMarker); // Elimina el marcador de selección existente
        }
        selectMarker = Instantiate(vertexMarkerPrefab, position, Quaternion.identity);
        selectMarker.GetComponent<Renderer>().material.color = selectColor;
    }

    void RemovePreselectMarker()
    {
        if (preselectMarker != null)
        {
            Destroy(preselectMarker);
            preselectMarker = null;
        }
    }

    void RemoveSelectMarker()
    {
        if (selectMarker != null)
        {
            Destroy(selectMarker);
            selectMarker = null;
        }
    }

    void MoveSelectedVertex(Vector3 newPosition)
    {
        Vector3 newPositionLocal = transform.InverseTransformPoint(newPosition);
        Vector3 originalVertexPosition = vertices[selectedVertexIndex];

        // Encuentra la posición local original del vértice seleccionado para comparar con otros vértices
        Vector3 displacement = newPositionLocal - originalVertexPosition;

        // Aplicar el desplazamiento a todos los vértices que comparten la misma posición original
        for (int i = 0; i < vertices.Length; i++)
        {
            if (vertices[i] == originalVertexPosition) // Comparación directa puede ser problemática debido a la precisión de punto flotante
            {
                vertices[i] += displacement;
            }
        }

        mesh.vertices = vertices;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        // Actualiza la posición del marcador de selección
        if (selectMarker != null)
        {
            selectMarker.transform.position = newPosition;
        }
    }


    int FindClosestVertexIndex(Vector3 hitPoint)
    {
        float closestDistanceSqr = Mathf.Infinity;
        int closestIndex = -1;
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 worldPosVertex = transform.TransformPoint(vertices[i]);
            float distanceSqr = (worldPosVertex - hitPoint).sqrMagnitude;
            if (distanceSqr < closestDistanceSqr)
            {
                closestDistanceSqr = distanceSqr;
                closestIndex = i;
            }
        }
        return closestIndex;
    }
    public void UpdateVertexPositions()
    {
        mesh = GetComponent<MeshFilter>().mesh; // Asegúrate de que esta línea obtiene la malla actualizada
        vertices = mesh.vertices; // Actualiza la lista de vértices con las nuevas posiciones
    }

    public void ReloadMeshData()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        // Asegúrate de que cualquier estado de selección también se actualice si es necesario
    }

}
