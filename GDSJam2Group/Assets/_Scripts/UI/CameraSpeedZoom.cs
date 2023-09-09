using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraSpeedZoom : MonoBehaviour
{
    public CinemachineVirtualCamera vCam;
    public CinemachineConfiner2D vConfiner;
    public Vector2 orthoRange;
    public Vector2 speedRange;
    public AnimationCurve speedToZoomCurve;
    public Rigidbody2D target;
    public float minStep;
    public float smoothing;

    void OnEnable()
    {
        PlayerController.OnMonkeyExpired += ClearTarget;
        MonkeyManager.OnMonkeySpawned += AquireTarget;
    }
    void OnDisable()
    {
        PlayerController.OnMonkeyExpired -= ClearTarget;
        MonkeyManager.OnMonkeySpawned -= AquireTarget;
    }

    void ClearTarget()
    {
        target = null;
    }

    void AquireTarget(GameObject newTarget)
    {
        target = newTarget.GetComponent<Rigidbody2D>();
        vCam.Follow = target.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;

        // Speed of target
        float currentSpeed = target.velocity.magnitude;
        
        // Normalize the speed within the SpeedRange
        float speedNormalized = Mathf.InverseLerp(speedRange.x, speedRange.y, currentSpeed);
        
        // Evaluate the animation curve to get the orthographic size within the OrthoRange
        float orthoSize = Mathf.Lerp(orthoRange.x, orthoRange.y, speedToZoomCurve.Evaluate(speedNormalized));
        float currentOrthoSize = vCam.m_Lens.OrthographicSize;

        float amountToMove = Mathf.Abs(currentOrthoSize - orthoSize);
        
        if (amountToMove < minStep) return;

        if (amountToMove > smoothing)
        {
            orthoSize = currentOrthoSize + (currentOrthoSize - orthoSize > 0 ? -smoothing : smoothing);
        }

        // Assign the calculated orthographic size to the vCam
        vCam.m_Lens.OrthographicSize = orthoSize;

        vConfiner.InvalidateCache();
    }
}
