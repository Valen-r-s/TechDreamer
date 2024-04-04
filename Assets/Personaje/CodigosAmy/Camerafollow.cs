using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
 
public class Camerafollow : MonoBehaviour
{
    public Transform Player; // tener referencia del que vamos a seguir
    public Vector3 offset; // de que distancia lo queremos ver
    public float cameraSpeed = 10f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate() 
    {
        Vector3 desiredPosition = Player.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, cameraSpeed*Time.deltaTime);

        //transform.position = Player.position + offset;
        transform.position = smoothPosition;  // para que se vea un poco mas suavizado

        //transform.LookAt(Player); // sirve depronto mas para el 2d uno se marea


    }
}
