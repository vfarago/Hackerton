using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

//  시네머신 가상 카메라 변경 Script
//  Priority 값이 가장 높은 카메라가 메인 카메라 

public class ChangeCamera : MonoBehaviour
{
    public CinemachineVirtualCamera roomCam;
   

    public void SetDefaultCamera()
    {
        roomCam.Priority = 9;
    }

    public void SetRoomCamera()
    {
        roomCam.Priority = 11;
    }

}
