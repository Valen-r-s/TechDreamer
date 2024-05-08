using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // El objetivo al que la cámara debe mirar
    public Vector3 offset; // Offset inicial de la cámara respecto al target
    public float rotationSpeed = 100f; // Velocidad de rotación de la cámara

    private Vector3 previousPosition;

    void Start()
    {
        // Configura la posición inicial de la cámara usando el offset
        transform.position = target.position + offset;
        transform.LookAt(target);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Right mouse button
        {
            previousPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 delta = Input.mousePosition - previousPosition;
            previousPosition = Input.mousePosition;

            float angleX = delta.x * rotationSpeed * Time.deltaTime; // Invertido
            float angleY = -delta.y * rotationSpeed * Time.deltaTime; // Invertido

            // Realiza la rotación alrededor del target
            RotateCamera(angleX, angleY);

            // Mantén la cámara en la esfera definida por el offset alrededor del target
            Vector3 desiredPosition = target.position + offset;
            transform.position = target.position + (transform.position - target.position).normalized * offset.magnitude;
        }
    }

    private void RotateCamera(float angleX, float angleY)
    {
        // Realiza la rotación alrededor del target
        transform.RotateAround(target.position, Vector3.up, angleX);
        transform.RotateAround(target.position, transform.right, angleY);

        // Asegúrate de que la cámara mire al target después de la rotación
        transform.LookAt(target);
    }
}
