using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEvent : MonoBehaviour
{
    // 고객 아바타가 문을 열고 방을 들어갈때까지 경로에 있는 Collider Trigger에 충돌할때 마다 이벤트 발생

    public GameObject door;


    private void OnTriggerEnter(Collider other)
    {
        switch(other.name)
        {
            case "ChangeAnimationPoint":
                GetComponent<ChangeCamera>().SetRoomCamera();
                door.GetComponent<ChangeDoor>().SetDoor(true);
                break;

            case "EndAnimationPoint":
                GetComponent<PlayerWayPoint>().StopMove();
                break;

        }

    }


}
