using Unity.Netcode;
using UnityEngine;

public class BodyRotation : NetworkBehaviour
{

    /*
        used in the player

        for Rotating left and right
    */

    [SerializeField]
    private float mouseSensitivity = 200f;

    private Transform playerBody;

    NetworkVariable<float> LocalRotaion = new NetworkVariable<float>();
    InputManager inputManager = InputManager.Instance;

    void Awake()
    {
        playerBody = transform.Find("Model");

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
    }

    void ClientInput()
    {
        //float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseX = inputManager.GetPlayerMouseDelta().x;

        UpdateClientRotationServerRpc(mouseX);

    }

    [ServerRpc]
    void UpdateClientRotationServerRpc(float mouseX)
    {
        LocalRotaion.Value = mouseX;
    }

    void UpdateServer()
    {
        playerBody.Rotate(Vector3.up * LocalRotaion.Value);

    }
}
