using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // El objetivo al que la c�mara debe mirar
    public Vector3 offset; // Offset inicial de la c�mara respecto al target
    public float rotationSpeed = 100f; // Velocidad de rotaci�n de la c�mara

    private Vector3 previousPosition;

    void Start()
    {
        // Configura la posici�n inicial de la c�mara usando el offset
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

            // Realiza la rotaci�n alrededor del target
            RotateCamera(angleX, angleY);

            // Mant�n la c�mara en la esfera definida por el offset alrededor del target
            Vector3 desiredPosition = target.position + offset;
            transform.position = target.position + (transform.position - target.position).normalized * offset.magnitude;
        }
    }

    private void RotateCamera(float angleX, float angleY)
    {
        // Realiza la rotaci�n alrededor del target
        transform.RotateAround(target.position, Vector3.up, angleX);
        transform.RotateAround(target.position, transform.right, angleY);

        // Aseg�rate de que la c�mara mire al target despu�s de la rotaci�n
        transform.LookAt(target);
    }
}
