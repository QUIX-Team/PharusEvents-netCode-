using Unity.Netcode;
using UnityEngine;

public class PlayerMovements : NetworkBehaviour
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

    // movement
    CharacterController controller;
    Vector3 velocity;
    NetworkVariable<float> xPosition = new NetworkVariable<float>();
    NetworkVariable<float> zPosition = new NetworkVariable<float>();

    // values
    [SerializeField] float movementsSpead = 12f;
    [SerializeField] Vector2 spawnRange = new Vector2(-10, 10);
    [SerializeField] float gravity = -20;
    [SerializeField] float jumpForece = 8f;

    //jumping
    Transform groundCheck;
    [SerializeField] LayerMask environmentMask;
    float groundDistance = 0.4f;
    //NetworkVariable<bool> isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        groundCheck = transform.Find("groundCheck").GetComponent<Transform>();

        transform.position = new Vector3(Random.Range(spawnRange.x, spawnRange.y), 0, Random.Range(spawnRange.x, spawnRange.y));

    }

    void Update()
    {
        //isGrounded.Value = Physics.CheckSphere(groundCheck.position, groundDistance, environmentMask);

        //if (isGrounded.Value && velocity.y < 0)
        //{
        //    velocity.y = -2f;
        //}

        if (IsServer)
        {
            UpdateServer();
        }

        if (IsClient && IsOwner)
        {
            UpdateClient();
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        //if (Input.GetButtonDown("Jump") && isGrounded.Value)
        //{
        //    velocity.y = jumpForece;
        //}
    }

    void UpdateServer()
    {
        Vector3 move = transform.right * xPosition.Value + transform.forward * zPosition.Value;

        controller.Move(move * movementsSpead * Time.deltaTime);
    }
    void UpdateClient()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;

        UpdateClientPositionServerRpc(x,z);
    }

    [ServerRpc]
    public void UpdateClientPositionServerRpc(float x, float z)
    {
        xPosition.Value = x;
        zPosition.Value = z;
    }
}
