using System.Collections.Generic;
using UnityEngine;

public class VertexComparisonScoreCalculatorT : MonoBehaviour
{
    public GameObject targetObject;
    public float tolerance = 0.05f; // Tolerancia para la coincidencia de vértices
    public int pointsPerVertex = 25; // Puntos por cada vértice coincidente

    // Lista de coordenadas de vértices de referencia
    private List<Vector3> referenceVertices = new List<Vector3>
    {
        new Vector3(0.7005677f, -0.4866788f, 0.7264161f),
        new Vector3(-0.6465729f, -0.4993614f, 0.7272461f),
        new Vector3(0.3108124f, 1.336882f, 0.6286133f),
        new Vector3(-0.2566956f, 1.339761f, 0.6259277f),
        new Vector3(0.3118958f, 1.336588f, -0.6347656f),
        new Vector3(-0.2715515f, 1.347459f, -0.6256836f),
        new Vector3(0.6970825f, -0.4895422f, -0.7097656f),
        new Vector3(-0.6787476f, -0.5048936f, -0.7128418f),
        new Vector3(0.6484436f, 0.6021037f, 0.3834961f),
        new Vector3(0.02705841f, 1.338321f, 0.6272705f),
        new Vector3(0.08280335f, 0.615056f, 0.3949707f),
        new Vector3(-0.6544922f, 0.6153452f, 0.4026367f),
        new Vector3(0.02699739f, -0.4930201f, 0.7268311f),
        new Vector3(0.3113541f, 1.336735f, -0.003076166f),
        new Vector3(0.02017212f, 1.342024f, -0.6302246f),
        new Vector3(0.01963043f, 1.342171f, 0.001464844f),
        new Vector3(-0.2641236f, 1.34361f, 0.0001220703f),
        new Vector3(0.6792023f, 0.6192815f, -0.4193359f),
        new Vector3(0.009167463f, -0.4972179f, -0.7113037f),
        new Vector3(0.06704407f, 0.6173045f, -0.4147949f),
        new Vector3(-0.6493164f, 0.6492345f, -0.436377f),
        new Vector3(0.6988251f, -0.4881105f, 0.008325219f),
        new Vector3(0.02525482f, -0.4944518f, 0.008740246f),
        new Vector3(-0.6626602f, -0.5021275f, 0.007202148f),
        new Vector3(-0.6321381f, 0.6108475f, 0.04404297f),
        new Vector3(0.6917236f, 0.6397074f, 0.02553711f)
    };

    // Método para calcular el puntaje basado en la coincidencia de vértices
    public float CalculateScore()
    {
        MeshFilter meshFilter = targetObject.GetComponent<MeshFilter>();
        if (meshFilter != null)
        {
            Mesh mesh = meshFilter.mesh;
            Vector3[] playerVerticesArray = mesh.vertices;
            HashSet<Vector3> playerVertices = new HashSet<Vector3>(playerVerticesArray);

            int matchingVertices = 0;

            foreach (var playerVertex in playerVertices)
            {
                foreach (var referenceVertex in referenceVertices)
                {
                    if (Vector3.Distance(playerVertex, referenceVertex) < tolerance)
                    {
                        matchingVertices++;
                        break;
                    }
                }
            }

            // Calcular el puntaje sumando 25 puntos por cada vértice coincidente
            float score = matchingVertices * pointsPerVertex;

            return score;
        }
        else
        {
            Debug.LogError("No MeshFilter found on the target object.");
            return 0;
        }
    }
}
