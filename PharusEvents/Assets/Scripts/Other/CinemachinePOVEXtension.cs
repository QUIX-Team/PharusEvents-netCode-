using Cinemachine;
using UnityEngine;

public class CinemachinePOVEXtension : CinemachineExtension
{

    Vector3 startingRotation;
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (vcam.Follow)
        {
            if(stage == CinemachineCore.Stage.Aim)
            {
                if(startingRotation == null)
                {
                    startingRotation = transform.localRotation.eulerAngles;
                }
            }
        }
    }
}
