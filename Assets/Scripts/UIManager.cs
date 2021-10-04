using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button btn_option;
    [SerializeField] private GameObject bg_option;
    [SerializeField] private Slider sl_option;
    [SerializeField] private Button backBtn;
    

    public GameObject brosherSet, adults, kids;

    private int pageNum;

    private float backTimer;



    //at OpeningListener
    public GameObject selBooth, selRoom;

    public Button experence, meeting, mr_reset;
    public Button[] btns;

    public GameObject mainLight, videoLight, openingPanel;
    public GameObject canvas;
    

    public Button resetBtn;

    ARPlaneManager arPlaneManager;
    RoomController doorTouch;
    public static int ROOMNUM { get; set; }

    private void Awake()
    {

        arPlaneManager = FindObjectOfType<ARPlaneManager>();
        doorTouch = arPlaneManager.GetComponent<RoomController>();

        btn_option.onClick.AddListener(() => OptionController());
        backBtn.onClick.AddListener(() =>
        {
            BackBtn();
        });
        sl_option.maxValue = 1.7f;
        sl_option.onValueChanged.AddListener(delegate
        {
            ChangeLight(sl_option.value);
        });
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

        brosherSet.SetActive(false);
        bg_option.SetActive(false);


        SetSession(false);
        selRoom.SetActive(false);
    }


    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                backTimer += Time.deltaTime;


                if (backTimer < 1.5f && Input.GetKeyDown(KeyCode.Escape))
                {
                    Application.Quit();
                }
            }
        }
    }

    private void OptionController()
    {
        bg_option.SetActive(!bg_option.activeSelf);

        if (Base.Instance.RoomController.roomLight != null)
        {
            sl_option.value = Base.Instance.RoomController.roomLight.intensity;
        }
    }

    private void ChangeLight(float value)
    {
        if (Base.Instance.RoomController.roomLight != null)
        {
            Base.Instance.RoomController.roomLight.intensity = value;
        }
    }

    public void MonitorTouch()
    {
        brosherSet.SetActive(true);
    }

    public void BackBtn()
    {
        SceneManager.LoadScene(0);
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