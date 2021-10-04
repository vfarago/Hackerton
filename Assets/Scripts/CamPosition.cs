using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamPosition : MonoBehaviour
{
    [SerializeField] private Text camDebug;
    [SerializeField] private Transform head;
    Camera main;

    private RoomController doorTouch;
    private bool isExDoor, isInDoor;
    private Vector2 start, end;

    private void Awake()
    {
        doorTouch = FindObjectOfType<RoomController>();

        isExDoor = false;
        isInDoor = false;
        main = Camera.main;
    }

    //private void OnCollisionExit(Collision collision)
    private void OnTriggerExit(Collider other)
    {
        if (other.name.Equals("ex_door"))
        {
            if (isInDoor)
            {
                isInDoor = false;
                isExDoor = false;

                doorTouch.SetRoom(false);
            }
            else
            {
                isExDoor = true;
            }
        }

        if (other.name.Equals("Door"))
        {
            if (isExDoor)
            {
                isExDoor = false;
            }
            else
            {
                isInDoor = true;
            }
        }
        camDebug.text = string.Format("ex:{0}, in:{1}, dd:{2}  pass", isExDoor, isInDoor, main.transform.eulerAngles);
    }

}
