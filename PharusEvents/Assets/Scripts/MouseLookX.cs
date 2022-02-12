using UnityEngine;

public class MouseLookX : MonoBehaviour
{

    /*
        used in the player

        access mouse sensitivity

        for looking left and right
    */

    [SerializeField]
    private float mouseSensitivity = 200f;

    private Transform playerBody;


    void Start()
    {
        playerBody = GetComponent<Transform>();

    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        playerBody.Rotate(Vector3.up * mouseX);

    }
}
