using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class OpeningListener : MonoBehaviour
{
    public GameObject selBooth, selRoom;

    public Button experence, meeting, mr_reset;
    public Button[] btns;

    public GameObject mainLight, videoLight, openingPanel;
    public GameObject canvas;

    public Button resetBtn;

    ARPlaneManager arPlaneManager;
    DoorTouch doorTouch;

    public static int ROOMNUM { get; set; }

    private void Awake()
    {
        arPlaneManager = FindObjectOfType<ARPlaneManager>();
        doorTouch = arPlaneManager.GetComponent<DoorTouch>();

        SetSession(false);
    }


    void Start()
    {
        mr_reset.onClick.AddListener(() => ResetRot());

        experence.onClick.AddListener(() =>
        {
            selBooth.SetActive(false);
            selRoom.SetActive(true);
        });
        meeting.onClick.AddListener(() =>
        {
            for (int i = 0; i < canvas.transform.childCount; i++)
            {
                canvas.transform.GetChild(i).gameObject.SetActive(false);
                if (i == 2 || i == 3)
                {
                    canvas.transform.GetChild(i).gameObject.SetActive(true);
                }
            }
            videoLight.SetActive(true);
            GameObject obj = Instantiate(Resources.Load("prefabs/MRPivot") as GameObject);
        });

        btns = selRoom.GetComponentsInChildren<Button>();

        for (int i = 0; i < btns.Length; i++)
        {
            int number = i;
            if (number > 2)
            {
                btns[number].interactable = false;
            }
            btns[number].onClick.AddListener(() =>
            {
                ROOMNUM = number;
                OnClickGo();
            });
        }

        selRoom.SetActive(false);
    }

    private void ResetRot()
    {
        FindObjectOfType<ObjectFix>().LookCam();
    }

    void OnClickGo()
    {
        openingPanel.SetActive(false);
        mainLight.SetActive(true);
        SetSession(true);
    }



    private void SetSession(bool on)
    {
        resetBtn.interactable = !on;
        arPlaneManager.enabled = on;
        doorTouch.enabled = on;
    }
}