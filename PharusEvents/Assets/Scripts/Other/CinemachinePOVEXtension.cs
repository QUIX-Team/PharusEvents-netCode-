using Cinemachine;
using UnityEngine;

public class CinemachinePOVEXtension : CinemachineExtension
{

    private InputManager inputManager;
    private Vector3 startingRotation;

    [SerializeField] private float clampAngel = 80f;
    [SerializeField] private float rotationSpeed = 10f;

    protected override void Awake()
    {
        inputManager = InputManager.Instance;
        base.Awake();

    }
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (vcam.Follow)
        {
            if (stage == CinemachineCore.Stage.Aim)
            {
                if (startingRotation == null)
                {
                    startingRotation = transform.localRotation.eulerAngles;
                    Vector2 deltaInput = inputManager.GetPlayerMouseDelta();

                    startingRotation.x += deltaInput.x * Time.deltaTime * rotationSpeed;
                    startingRotation.y += deltaInput.y * Time.deltaTime * rotationSpeed;
                    startingRotation.y = Mathf.Clamp(startingRotation.y, -clampAngel, clampAngel);

                    state.RawOrientation = Quaternion.Euler(-startingRotation.y, startingRotation.x, 0f);
                }
            }
        }
    }
}
