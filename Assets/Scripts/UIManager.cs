using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button btn_option;
    [SerializeField] private GameObject bg_option;
    [SerializeField] private Slider sl_option;
    [SerializeField] private Button backBtn;
    

    public GameObject brosherSet, adults, kids;

    private int pageNum;

    private float backTimer;
    private void Awake()
    {       

        btn_option.onClick.AddListener(() => OptionController());

        sl_option.maxValue = 1.7f;
        sl_option.onValueChanged.AddListener(delegate
        {
            ChangeLight(sl_option.value);
        });

        //adults.GetComponent<Brosher>().sprs = Resources.LoadAll<Material>("Brosher/Adults/Materials");
        //kids.GetComponent<Brosher>().sprs = Resources.LoadAll<Material>("Brosher/Kids/Materials");

        //임시로 만든 버튼, 후에 수정 예정
        backBtn.onClick.AddListener(() =>
        {
            BackBtn();
        });

        //brosherSet.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
        //{
        //    pageNum = 0;
        //    adults.SetActive(true);
        //    adults.transform.GetChild(0).GetComponent<Image>().sprite = adults.GetComponent<Brosher>().sprs[0];
        //    brosherSet.SetActive(false);
        //});
        //brosherSet.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() =>
        //{
        //    pageNum = 0;
        //    kids.SetActive(true);
        //    kids.transform.GetChild(0).GetComponent<Image>().sprite = kids.GetComponent<Brosher>().sprs[0];
        //    brosherSet.SetActive(false);
        //});
        //brosherSet.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() =>
        //{
        //    brosherSet.SetActive(false);
        //});

        //adults.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() =>
        //{
        //    if (pageNum != 0)
        //    {
        //        pageNum--;
        //        adults.transform.GetChild(0).GetComponent<Image>().sprite = adults.GetComponent<Brosher>().sprs[pageNum];
        //    }
        //});
        //adults.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() =>
        //{
        //    if (pageNum < adults.GetComponent<Brosher>().sprs.Length-1)
        //    {
        //        pageNum++;
        //        adults.transform.GetChild(0).GetComponent<Image>().sprite = adults.GetComponent<Brosher>().sprs[pageNum];
        //    }
        //});

        //kids.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() =>
        //{
        //    if (pageNum != 0)
        //    {
        //        pageNum--;
        //        kids.transform.GetChild(0).GetComponent<Image>().sprite = kids.GetComponent<Brosher>().sprs[pageNum];
        //    }
        //});
        //kids.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() =>
        //{
        //    if (pageNum < kids.GetComponent<Brosher>().sprs.Length-1)
        //    {
        //        pageNum++;
        //        kids.transform.GetChild(0).GetComponent<Image>().sprite = kids.GetComponent<Brosher>().sprs[pageNum];
        //    }

        //});

        //adults.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() =>
        //{
        //    adults.transform.GetChild(0).GetComponent<Image>().sprite = null;
        //    adults.SetActive(false);
        //    pageNum = 0;
        //    brosherSet.SetActive(true);
        //});
        //kids.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() =>
        //{
        //    kids.transform.GetChild(0).GetComponent<Image>().sprite = null;
        //    kids.SetActive(false);
        //    pageNum = 0;
        //    brosherSet.SetActive(true);
        //});

        brosherSet.SetActive(false);
        //adults.SetActive(false);
        //kids.SetActive(false);

        bg_option.SetActive(false);
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

        if (Base.Instance.DoorTouch.roomLight != null)
        {
            sl_option.value = Base.Instance.DoorTouch.roomLight.intensity;
        }
    }

    private void ChangeLight(float value)
    {
        if (Base.Instance.DoorTouch.roomLight != null)
        {
            Base.Instance.DoorTouch.roomLight.intensity = value;
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
}