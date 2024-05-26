using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveUniqueVerticesOnKeyPress : MonoBehaviour
{
    public GameObject targetObject;
    public string fileName = "VerticesCoordinates.txt";
    public KeyCode saveKey = KeyCode.Q;

    void Update()
    {
        if (Input.GetKeyDown(saveKey))
        {
            SaveUniqueVerticesToFile();
        }
    }

    void SaveUniqueVerticesToFile()
    {
        MeshFilter meshFilter = targetObject.GetComponent<MeshFilter>();
        if (meshFilter != null)
        {
            Mesh mesh = meshFilter.mesh;
            Vector3[] vertices = mesh.vertices;
            HashSet<Vector3> uniqueVertices = new HashSet<Vector3>();

            foreach (var vertex in vertices)
            {
                uniqueVertices.Add(vertex);
            }

            using (StreamWriter writer = new StreamWriter(fileName))
            {
                foreach (var vertex in uniqueVertices)
                {
                    string line = vertex.x + "," + vertex.y + "," + vertex.z;
                    writer.WriteLine(line);
                }
            }

            Debug.Log("Vertices saved to " + fileName);
        }
        else
        {
            Debug.LogError("No MeshFilter found on the target object.");
        }
    }
}
