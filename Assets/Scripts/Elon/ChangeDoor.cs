using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDoor : MonoBehaviour
{
    //  문 열고 닫는 Script 
    //  Kevin님이 만든 RoomController에 있는 DoorAction 코루틴에 접근할 방법 못찾아서 따로 만듬
    public void SetDoor(bool _isOpen)
    {
        StartCoroutine(ChangeDoorAction(_isOpen));
    }

    IEnumerator ChangeDoorAction(bool _isOpen)
    {
        float start = 0f;
        float end = -175f;

        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime;
            float turn = Mathf.Lerp(start, end, time);
            transform.localEulerAngles = new Vector3(0, turn, 0);

            yield return new WaitForEndOfFrame();
        }
    }
}
