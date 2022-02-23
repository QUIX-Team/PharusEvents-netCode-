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

    NetworkVariable<float> LocalRotaion;

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
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

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
