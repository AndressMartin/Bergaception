using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    CinemachineVirtualCamera virtualCamera;
    Cinemachine3rdPersonFollow personFollow;
    public VoidEvent EndCinematic;
    Vector3 defaultPos;
    [SerializeField] bool playingAnimation;
    public void ResetVirtualCamBehindPlayer(CinemachineVirtualCamera virtualCam)
    {
        virtualCamera = virtualCam;
        personFollow = virtualCam.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        defaultPos = new Vector3(personFollow.ShoulderOffset.x,
                0.25f,
                personFollow.ShoulderOffset.z);
        playingAnimation = true;
    }

    private void Update()
    {
        if (playingAnimation)
        {
            if (personFollow.ShoulderOffset.y - defaultPos.y <= 0.025f)
            {
                playingAnimation = false;
                EndCinematic.Raise();
                return;
            }
            personFollow.ShoulderOffset = Vector3.Lerp(personFollow.ShoulderOffset, defaultPos, 2 * Time.deltaTime);
        }
    }
}
