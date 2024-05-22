using System.Collections.Generic;
using UnityEngine;

public class VertexScoreCalculator : MonoBehaviour
{
    public Texture2D frontReferenceImage;
    public Texture2D sideReferenceImage;
    public VertexProjector vertexProjector;
    public float closeThreshold = 0.01f; // Umbral para puntos cercanos
    public float mediumThreshold = 0.05f; // Umbral para puntos medios

    public float CalculateFinalScore()
    {
        float frontScore = CalculateScoreForView(vertexProjector.ProjectVertices(vertexProjector.frontCamera, vertexProjector.frontPlane), frontReferenceImage);
        float sideScore = CalculateScoreForView(vertexProjector.ProjectVertices(vertexProjector.sideCamera, vertexProjector.sidePlane), sideReferenceImage);

        // Promediar las puntuaciones de ambas vistas
        float finalScore = (frontScore + sideScore) / 2;
        return finalScore;
    }

    private float CalculateScoreForView(List<Vector2> projectedVertices, Texture2D referenceImage)
    {
        int totalPoints = projectedVertices.Count;
        int score = 0;

        foreach (var point in projectedVertices)
        {
            int x = (int)(point.x * referenceImage.width);
            int y = (int)(point.y * referenceImage.height);
            Color pixelColor = referenceImage.GetPixel(x, y);

            // Distancia al contorno
            float distance = CalculateDistanceToEdge(referenceImage, x, y);

            if (distance <= closeThreshold)
            {
                score += 3; // Máxima puntuación
            }
            else if (distance <= mediumThreshold)
            {
                score += 2; // Puntuación media
            }
            else if (pixelColor == Color.black)
            {
                score += 1; // Puntuación mínima
            }
        }

        return (float)score / (totalPoints * 3) * 100; // Normalizar la puntuación a un rango de 0 a 100
    }

    private float CalculateDistanceToEdge(Texture2D texture, int x, int y)
    {
        // Implementa una función para calcular la distancia al borde más cercano de la imagen de referencia
        // Esto puede implicar buscar píxeles negros en un radio alrededor del punto (x, y)
        // Para simplificar, asumiremos que el contorno es negro
        float minDistance = float.MaxValue;

        for (int i = Mathf.Max(0, x - 10); i < Mathf.Min(texture.width, x + 10); i++)
        {
            for (int j = Mathf.Max(0, y - 10); j < Mathf.Min(texture.height, y + 10); j++)
            {
                if (texture.GetPixel(i, j) == Color.black)
                {
                    float distance = Vector2.Distance(new Vector2(x, y), new Vector2(i, j));
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                    }
                }
            }
        }

        return minDistance / texture.width; // Normalizar la distancia
    }
}
