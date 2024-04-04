using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amy_Mov : MonoBehaviour
{

    public float RunSpeed = 2.5f;
    public float rotationSpeed = 250;

    public Animator animator;

    private float x, y;

    public Rigidbody rb;
    public float jumpHeight = 3;

    public Transform groundCheck;
    public float groundDistance = 0.1f;
    public LayerMask groundMask;

    bool isGrounded;

    private bool isDancing = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");   // para cuadno lo haga 3d y no olvidar poner la camara para que funcione


        //y = Input.GetAxis("Horizontal");



        transform.Rotate(0, x * Time.deltaTime * rotationSpeed, 0);
        transform.Translate(0,0, y* Time.deltaTime* RunSpeed);

        animator.SetFloat("VelX", x);
        animator.SetFloat("VelY", y);  // para cuadno lo haga 3d

        //animator.SetFloat("VelX", x);
        //animator.SetFloat("VelY", y);  // para cuando lo quiera solo en 2d



        if (Input.GetKey("f"))
        {
            animator.Play("Dancing");
        }
        if (x > 0 || x < 0 || y > 0 || y < 0)
        {
            animator.SetBool("isDancing", true);
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(Input.GetKey("space") && isGrounded)
        {
            animator.Play("Jumping");
            Invoke("Jumping", 1f);

        }
    }

    public void Jump()
    {
        rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse); //nuestro rrg se le añada una fuerza hacia arriba con un modo de impulso
    }
}
