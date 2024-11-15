using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSwitcher : MonoBehaviour
{
    public MonoBehaviour vertexSelector; // Usar MonoBehaviour si hay problemas de referencia.
    public MonoBehaviour edgeSelector;

    private void Start()
    {
        if (vertexSelector != null && edgeSelector != null)
        {
            vertexSelector.enabled = true;
            edgeSelector.enabled = false;
        }
        else
        {
            Debug.LogError("References to VertexSelector and EdgeSelector are not set in ModeSwitcher.");
        }
    }

    public void ActivateVertexMode()
    {
        if (vertexSelector != null) vertexSelector.enabled = true;
        if (edgeSelector != null) edgeSelector.enabled = false;
    }

    public void ActivateEdgeMode()
    {
        if (vertexSelector != null) vertexSelector.enabled = false;
        if (edgeSelector != null) edgeSelector.enabled = true;
    }

}

