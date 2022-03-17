using Assets.Scripts.Player;
using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{

    private CharacterController controller;
    Vector3 velocity;

    [SerializeField] private float playerSpeed = 12f;
    private int gravityValue = -10;
    [SerializeField] Vector2 spawnRange = new Vector2(-10, 10);

    NetworkVariable<float> xPosition = new NetworkVariable<float>();
    NetworkVariable<float> zPosition = new NetworkVariable<float>();
    NetworkVariable<Vector3> FMovements = new NetworkVariable<Vector3>();

    NetworkVariable<PlayerState> playerState = new NetworkVariable<PlayerState>();
    private Animator animator;

    private InputManager inputManager;
    private Transform cameraTransform;

    void Awake()
    {
        controller = GetComponent<CharacterController>();

        animator = transform.Find("Model").GetComponent<Animator>();

        inputManager = InputManager.Instance;

    }

    private void Start()
    {
        cameraTransform = GameObject.Find("MainCamira").transform;

        if (IsClient && IsOwner)
        {
            transform.position = new Vector3(Random.Range(spawnRange.x, spawnRange.y), 0, Random.Range(spawnRange.x, spawnRange.y));

            PlayerCameraFollow.Instance.FollowPlayer(transform.Find("CameraRoot"), transform.Find("LookAtTarget"));
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

        velocity.y = gravityValue;

        controller.Move(velocity);

    }


    private void ClientInput()
    {
        Vector2 input = inputManager.GetPlayerMovement();

        Vector3 move = new Vector3(input.x, 0, input.y);

        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move.y = 0;


        UpdateClientPositionServerRpc(move);

        if(input != Vector2.zero)
        {
            UpdatePlayerStateServerRpc(PlayerState.Walk);

        }
        else
        {
            UpdatePlayerStateServerRpc(PlayerState.Idle);
        }

    }

    [ServerRpc]
    private void UpdateClientPositionServerRpc(Vector3 move)
    {
        FMovements.Value = move;
    }

    private void UpdateServer()
    {
        controller.Move(FMovements.Value * Time.deltaTime * playerSpeed);

    }

    void ClientVisuals()
    {
        if (playerState.Value == PlayerState.Walk)
        {
            animator.SetBool("walking", true);
        }
        else if (playerState.Value == PlayerState.Idle)
        {
            animator.SetBool("walking", false);
        }
    }

    [ServerRpc]
    private void UpdatePlayerStateServerRpc(PlayerState state)
    {
        playerState.Value = state;

    }

}