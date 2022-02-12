using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    /*
        used in the player
    
        access movementsSpead
        access gravity (local)
        access jumpForce
        access layerMask

        for moving the player
        for appling physics

     */

    CharacterController controller;

    [SerializeField] float movementsSpead = 12f;
    Vector3 velocity;
    [SerializeField] float gravity = -20;
    [SerializeField] float jumpForece = 8f;

    Transform groundCheck;
    [SerializeField] LayerMask environmentMask;

    float groundDistance = 0.4f;
    bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        groundCheck = transform.Find("groundCheck").GetComponent<Transform>();
        
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, environmentMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * movementsSpead * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = jumpForece;
        }
    }
}
