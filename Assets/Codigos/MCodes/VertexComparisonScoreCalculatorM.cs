using System.Collections.Generic;
using UnityEngine;

public class VertexComparisonScoreCalculatorM : MonoBehaviour
{
    public GameObject targetObject;
    public float tolerance = 0.04f; // Tolerancia para la coincidencia de vértices
    public int pointsPerVertex = 25; // Puntos por cada vértice coincidente

    // Lista de coordenadas de vértices de referencia
    private List<Vector3> referenceVertices = new List<Vector3>
    {
        new Vector3(0.6638214f, -0.4938757f, 0.8895508f),
        new Vector3(-0.6448705f, -0.5168653f, 0.8946541f),
        new Vector3(0.3619934f, 0.8694957f, 0.8510742f),
        new Vector3(-0.327356f, 0.8712306f, 0.8650391f),
        new Vector3(0.3300842f, 0.8893058f, 0.09145508f),
        new Vector3(-0.3262939f, 0.8861139f, 0.09111328f),
        new Vector3(0.6797608f, -0.5121809f, -1.049561f),
        new Vector3(-0.6438233f, -0.5085484f, -1.050146f),
        new Vector3(0.3862244f, 0.3048315f, 0.8621582f),
        new Vector3(0.04204407f, 0.8661401f, 0.8608887f),
        new Vector3(0.1249554f, 0.1848272f, 0.8702671f),
        new Vector3(-0.3440277f, 0.3136476f, 0.8624024f),
        new Vector3(0.0317668f, -0.5127698f, 0.8884773f),
        new Vector3(0.3658051f, 0.8797624f, 0.4632324f),
        new Vector3(-0.05256043f, 0.8869489f, 0.08325195f),
        new Vector3(0.02442592f, 0.8772187f, 0.4646969f),
        new Vector3(-0.3312469f, 0.8798068f, 0.4633789f),
        new Vector3(0.3842316f, 0.318903f, 0.05493164f),
        new Vector3(0.03185615f, -0.5178277f, -1.05146f),
        new Vector3(-0.07104492f, 0.3153537f, 0.0616211f),
        new Vector3(-0.3478058f, 0.3205466f, 0.06479492f),
        new Vector3(0.6821839f, -0.5144369f, -0.05546875f),
        new Vector3(0.03231728f, -0.5168958f, -0.07868353f),
        new Vector3(-0.6453316f, -0.5177972f, -0.07812199f),
        new Vector3(-0.3607849f, 0.3008898f, 0.5134277f),
        new Vector3(0.3860626f, 0.3066263f, 0.5061035f),
        new Vector3(0.5677795f, -0.0169665f, 0.8660156f),
        new Vector3(0.2555899f, 0.2448293f, 0.8662127f),
        new Vector3(0.3943884f, -0.1545243f, 0.8799089f),
        new Vector3(0.2020187f, 0.8678179f, 0.8559815f),
        new Vector3(0.2141342f, 0.5854858f, 0.8615234f),
        new Vector3(0.3741089f, 0.5871636f, 0.8566163f),
        new Vector3(-0.1012003f, 0.5280289f, 0.8676531f),
        new Vector3(0.08349974f, 0.5254836f, 0.8655779f),
        new Vector3(-0.1426559f, 0.8686854f, 0.8629639f),
        new Vector3(0.07836111f, -0.1639713f, 0.8793722f),
        new Vector3(0.3477941f, -0.5033228f, 0.8890141f),
        new Vector3(-0.3356918f, 0.5924391f, 0.8637207f),
        new Vector3(-0.1095362f, 0.2492374f, 0.8663347f),
        new Vector3(-0.3065519f, -0.5148175f, 0.8915657f),
        new Vector3(-0.1561305f, -0.09956111f, 0.8754399f),
        new Vector3(-0.5539154f, -0.003501189f, 0.8737305f),
        new Vector3(0.3638992f, 0.874629f, 0.6571533f),
        new Vector3(0.1951155f, 0.8784906f, 0.4639646f),
        new Vector3(0.1932097f, 0.8733572f, 0.6578856f),
        new Vector3(0.1387619f, 0.8881273f, 0.08735351f),
        new Vector3(0.1566223f, 0.8833556f, 0.2732422f),
        new Vector3(0.3479446f, 0.8845341f, 0.2773438f),
        new Vector3(-0.150934f, 0.8816663f, 0.2779051f),
        new Vector3(-0.01406725f, 0.8820838f, 0.2739744f),
        new Vector3(-0.1894272f, 0.8865314f, 0.08718262f),
        new Vector3(0.033235f, 0.8716794f, 0.6627928f),
        new Vector3(-0.3287705f, 0.8829604f, 0.2772461f),
        new Vector3(-0.1534105f, 0.8785127f, 0.4640379f),
        new Vector3(-0.1446014f, 0.8729734f, 0.6621338f),
        new Vector3(-0.3293014f, 0.8755187f, 0.664209f),
        new Vector3(0.3571579f, 0.6041044f, 0.07319336f),
        new Vector3(0.1565933f, 0.3171284f, 0.05827637f),
        new Vector3(0.1295197f, 0.6023297f, 0.07653809f),
        new Vector3(0.3558084f, -0.5150043f, -1.05051f),
        new Vector3(0.2648499f, -0.01314428f, -0.9528809f),
        new Vector3(0.5667267f, -0.02420993f, -0.945459f),
        new Vector3(-0.1966849f, -0.002528492f, -0.9536098f),
        new Vector3(0.1411549f, -0.007168178f, -0.9542664f),
        new Vector3(-0.3059835f, -0.5131881f, -1.050803f),
        new Vector3(-0.06180267f, 0.6011513f, 0.07243653f),
        new Vector3(-0.5423828f, 0.003727686f, -0.9539551f),
        new Vector3(-0.2094254f, 0.3179502f, 0.06320801f),
        new Vector3(-0.2001831f, 0.6037477f, 0.07402344f),
        new Vector3(-0.3370499f, 0.6033303f, 0.0779541f),
        new Vector3(0.6809723f, -0.5133089f, -0.5525147f),
        new Vector3(0.3572506f, -0.5156664f, -0.06707614f),
        new Vector3(0.356039f, -0.5145383f, -0.564122f),
        new Vector3(0.3569753f, -0.5136033f, 0.4165043f),
        new Vector3(0.6730026f, -0.5041563f, 0.417041f),
        new Vector3(-0.3062766f, -0.5168805f, 0.4079853f),
        new Vector3(0.03204204f, -0.5148328f, 0.4048969f),
        new Vector3(0.03208672f, -0.5173618f, -0.5650715f),
        new Vector3(-0.6451011f, -0.5173312f, 0.4082661f),
        new Vector3(-0.3065072f, -0.5173465f, -0.07840276f),
        new Vector3(-0.3067377f, -0.5178125f, -0.5647908f),
        new Vector3(-0.6445774f, -0.5131728f, -0.5641342f),
        new Vector3(-0.3524063f, 0.3072687f, 0.6879151f),
        new Vector3(-0.529007f, 0.001674163f, 0.7331055f),
        new Vector3(-0.3376373f, 0.5967272f, 0.6628907f),
        new Vector3(-0.3435394f, 0.5935019f, 0.3022705f),
        new Vector3(-0.3460159f, 0.5903483f, 0.4884033f),
        new Vector3(-0.5432434f, -0.02079576f, 0.2513672f),
        new Vector3(-0.3542954f, 0.3107182f, 0.2891113f),
        new Vector3(-0.5408966f, 0.00365907f, 0.03129883f),
        new Vector3(0.3851471f, 0.3127646f, 0.2805176f),
        new Vector3(0.5698425f, -0.01889099f, -0.2028809f),
        new Vector3(0.3750183f, 0.5993327f, 0.259082f),
        new Vector3(0.374028f, 0.588061f, 0.6785889f),
        new Vector3(0.3759338f, 0.5931943f, 0.484668f),
        new Vector3(0.5760315f, -0.01500448f, 0.3095703f),
        new Vector3(0.3861435f, 0.3057289f, 0.6841309f),
        new Vector3(0.5470886f, -0.02252487f, 0.6326172f)
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
