using Assets.Scripts.Player;
using Unity.Netcode;
using UnityEngine;

public class PlayerMovements : NetworkBehaviour
{
    /*
        used in the player
    
        access PlayerCameraFollow (Singlton)

        for moving the player
        

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


    // animation
    NetworkVariable<PlayerState> playerState = new NetworkVariable<PlayerState>();
    private Animator animator;

    void Awake()
    {
        controller = GetComponent<CharacterController>();

        animator = transform.Find("Model").GetComponent<Animator>();

        
    }
    void Start()
    {
        if(IsClient && IsOwner)
        {
            transform.position = new Vector3(Random.Range(spawnRange.x, spawnRange.y), 0, Random.Range(spawnRange.x, spawnRange.y));

            PlayerCameraFollow.Instance.FollowPlayer(transform.Find("CameraRoot"));
        }

    }

    void Update()
    {

        if (IsServer)
        {
            UpdateServer();
        }

        if (IsClient && IsOwner)
        {
            ClientInput();
        }

        ClientVisuals();

        velocity.y = gravity;

        controller.Move(velocity);

    }

    void UpdateServer()
    {
        Vector3 move = transform.right * xPosition.Value + transform.forward * zPosition.Value;

        controller.Move(move * movementsSpead * Time.deltaTime);
    }
    void ClientInput()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        UpdateClientPositionServerRpc(x,z);

        if(x !=0 || z != 0)
        {
            UpdatePlayerStateServerRpc(PlayerState.Walk);
        }
        else
        {
            UpdatePlayerStateServerRpc(PlayerState.Idle);
        }
    }

    [ServerRpc]
    public void UpdateClientPositionServerRpc(float x, float z)
    {
        xPosition.Value = x;
        zPosition.Value = z;
    }

    void ClientVisuals()
    {
        if(playerState.Value == PlayerState.Walk)
        {
            animator.SetBool("walking", true);
        }
        else if(playerState.Value == PlayerState.Idle)
        {
            animator.SetBool("walking", false);
        }
    }

    [ServerRpc]
    public void UpdatePlayerStateServerRpc(PlayerState state)
    {
        playerState.Value = state;
    }
}
