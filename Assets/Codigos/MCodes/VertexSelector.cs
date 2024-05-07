using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshCollider))]
public class VertexSelector : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject vertexMarkerPrefab; // Prefab para el marcador de v�rtice
    private GameObject preselectMarker; // Marcador para el v�rtice preseleccionado
    private GameObject selectMarker; // Marcador para el v�rtice seleccionado
    private Mesh mesh;
    private Vector3[] vertices;
    private bool vertexSelected = false;
    private int selectedVertexIndex = -1;
    private int preselectedVertexIndex = -1; // �ndice del v�rtice preseleccionado
    private Vector3 initialMousePosition;
    private Vector3 initialVertexPosition;

    public Color preselectColor = Color.yellow; // Color para preselecci�n
    public Color selectColor = Color.green; // Color para selecci�n

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
                RemoveSelectMarker(); // Aseg�rese de eliminar el marcador de selecci�n al finalizar el movimiento
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
        // Eliminar el marcador de preselecci�n si existe
        RemovePreselectMarker();

        if (selectMarker != null)
        {
            Destroy(selectMarker); // Elimina el marcador de selecci�n existente
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

        // Encuentra la posici�n local original del v�rtice seleccionado para comparar con otros v�rtices
        Vector3 displacement = newPositionLocal - originalVertexPosition;

        // Aplicar el desplazamiento a todos los v�rtices que comparten la misma posici�n original
        for (int i = 0; i < vertices.Length; i++)
        {
            if (vertices[i] == originalVertexPosition) // Comparaci�n directa puede ser problem�tica debido a la precisi�n de punto flotante
            {
                vertices[i] += displacement;
            }
        }

        mesh.vertices = vertices;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        // Actualiza la posici�n del marcador de selecci�n
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
        mesh = GetComponent<MeshFilter>().mesh; // Aseg�rate de que esta l�nea obtiene la malla actualizada
        vertices = mesh.vertices; // Actualiza la lista de v�rtices con las nuevas posiciones
    }

    public void ReloadMeshData()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        // Aseg�rate de que cualquier estado de selecci�n tambi�n se actualice si es necesario
    }

}
