using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWayPoint : MonoBehaviour
{
    //  Waypoint 방식 움직임    
    
    public GameObject[] waypointArr;    //경로 지점 저장해둔 배열
    public bool isChangeAnimation;      //애니메이션 변경에 쓰는 bool값 변수

    [SerializeField]
    private bool isMove = true;         //움직임 판단
    [SerializeField]
    private float speed = 1f;           

    private Animator animator;
    private Vector3 currentPosition;    //현재 위치
    private int waypointIndex;          //총 지점의 수

    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(DoorEvent());        
    }

    public void StopMove()
    {
        isChangeAnimation = true;
        animator.SetBool("isMove",false);
        isMove = false;
    }

    IEnumerator DoorEvent()
    {
        while(isMove)
        {
            currentPosition = transform.localPosition;

            if (waypointIndex < waypointArr.Length && !isChangeAnimation)
            {
                float step = speed * Time.deltaTime;
                transform.localPosition = Vector3.MoveTowards(currentPosition, waypointArr[waypointIndex].transform.localPosition, step);

                if (0f == Vector3.Distance(waypointArr[waypointIndex].transform.localPosition, currentPosition))
                    waypointIndex++;              

            }

            yield return null;
        }
        yield break;
    }
}
