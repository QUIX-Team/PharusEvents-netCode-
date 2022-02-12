using UnityEngine;

public class MouseLookY : MonoBehaviour
{

    /*
        used in the camera

        access mouse sensitivity

        for looking up and down
     
     */
    [SerializeField]
    private float mouseSensitivity = 200f;

    float xRotation = 0;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
