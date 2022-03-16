using UnityEngine;

public class PlayerCameraFollow : MonoBehaviour
{
    public static PlayerCameraFollow Instance { get; private set; }

    private Cinemachine.CinemachineVirtualCamera cinemachineVirtualCamera;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        cinemachineVirtualCamera = GetComponent<Cinemachine.CinemachineVirtualCamera>();
    }

    public void FollowPlayer(Transform follow, Transform lookAt)
    {
        cinemachineVirtualCamera.Follow = follow;
        cinemachineVirtualCamera.LookAt = lookAt;
    }

}
