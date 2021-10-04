using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Video;

public class DoorTouch : MonoBehaviour
{
    [SerializeField] private GameObject initGo, bubble;
    [SerializeField] private GameObject[] roomInit;
    [SerializeField] private Text debug;
    
    private GameObject go_door, go_room, doorOpen;
    private Vector2 center;

    [HideInInspector] public Vector3 pos_door;
    [HideInInspector] public Light roomLight;
    public bool isRoom;

    public GameObject particle;
    
    private int pageNum;
    private bool touchTrigger;
    private void Awake()
    {       
        isRoom = false;

        //go_door = Instantiate(roomInit);
        go_door = Instantiate(initGo);

        go_door.transform.localScale = Vector3.zero;
        bubble.SetActive(false);

        center = new Vector2(Screen.width * 0.5f, Screen.height * 0.2f);        
        touchTrigger = false;
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.touchCount > 0)
        {
            Ray tRay = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(tRay, out RaycastHit tHit, 100f))
            {
                if (tHit.collider.name.Contains("Door"))
                {
                    EnterTheRoom(tHit.collider.gameObject);
                }
                else
                {
                    if (Input.GetTouch(0).phase.Equals(TouchPhase.Began))
                        WhatisThis(true, tHit.collider.name);
                }

                debug.text = string.Format("터치한거 {0}", tHit.collider.name);

                if (!touchTrigger)
                {
                    touchTrigger = true;

                    if (tHit.collider.name.Contains("ShinhanShpere"))
                    {
                        GameObject obj = particle;
                        if (tHit.collider.gameObject.transform.localScale.x > 0.5f)
                        {
                            obj.transform.localScale = new Vector3(2f, 2f, 2f);
                        }
                        else
                        {
                            obj.transform.localScale = new Vector3(1f, 1f, 1f);
                        }

                        Instantiate(obj, tHit.collider.gameObject.transform.position, tHit.collider.gameObject.transform.rotation);
                    }

                    if (tHit.collider.name == "Prev")
                    {

                        if (pageNum > 0)
                        {
                            pageNum--;
                            if (tHit.collider.gameObject.transform.parent.parent.GetChild(1).gameObject.activeSelf)
                            {
                                tHit.collider.gameObject.transform.parent.parent.GetChild(1).gameObject.GetComponent<MeshRenderer>().material                                    
                                    = Base.Instance.AdultMaterialArr[pageNum];
                            }
                            else if (tHit.collider.gameObject.transform.parent.parent.GetChild(2).gameObject.activeSelf)
                            {
                                tHit.collider.gameObject.transform.parent.parent.GetChild(2).gameObject.GetComponent<MeshRenderer>().material
                                    = Base.Instance.KidMaterialArr[pageNum];
                            }
                        }
                    }

                    if (tHit.collider.name == "Next")
                    {
                        if (tHit.collider.gameObject.transform.parent.parent.GetChild(1).gameObject.activeSelf)
                        {
                            if (pageNum < Base.Instance.AdultMaterialArr.Length - 1)
                            {
                                pageNum++;
                                tHit.collider.gameObject.transform.parent.parent.GetChild(1).gameObject.GetComponent<MeshRenderer>().material
                                    = Base.Instance.AdultMaterialArr[pageNum];
                            }
                        }
                        else if (tHit.collider.gameObject.transform.parent.parent.GetChild(2).gameObject.activeSelf)
                        {
                            if (pageNum < Base.Instance.KidMaterialArr.Length - 1)
                            {
                                pageNum++;
                                tHit.collider.gameObject.transform.parent.parent.GetChild(2).gameObject.GetComponent<MeshRenderer>().material
                                    = Base.Instance.AdultMaterialArr[pageNum];
                            }
                        }
                    }

                    if (tHit.collider.name == "Adult")
                    {
                        if (!tHit.collider.gameObject.transform.parent.GetChild(3).gameObject.activeSelf)
                        {
                            tHit.collider.gameObject.transform.parent.GetChild(3).gameObject.SetActive(true);
                            if (!tHit.collider.gameObject.transform.parent.GetChild(3).gameObject.GetComponent<VideoPlayer>().isPlaying)
                                tHit.collider.gameObject.transform.parent.GetChild(3).gameObject.GetComponent<VideoPlayer>().Play();
                        }
                        else
                        {
                            if (tHit.collider.gameObject.transform.parent.GetChild(3).gameObject.GetComponent<VideoPlayer>().isPlaying)
                            {
                                tHit.collider.gameObject.transform.parent.GetChild(3).gameObject.GetComponent<VideoPlayer>().Pause();
                            }
                            else
                            {
                                tHit.collider.gameObject.transform.parent.GetChild(3).gameObject.GetComponent<VideoPlayer>().Play();
                            }
                        }
                    }

                }
            }

            if (Input.GetTouch(0).phase.Equals(TouchPhase.Ended))
            {
                WhatisThis(false, string.Empty);
            }
        }
        if (Input.touchCount == 0 && touchTrigger) touchTrigger = false;

        if (isRoom)
            return;

        if (Input.touchCount > 0)
            return;

        Ray ray = Camera.main.ScreenPointToRay(center);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            if (hit.collider.name.Contains("ARPlane"))
            {
                CreateCube(hit.point, hit.collider.transform.eulerAngles);
            }
        }
    }    


    private void WhatisThis(bool on, string name)
    {
        bubble.SetActive(on);
        bubble.GetComponentInChildren<Text>().text = name;
    }

    private void EnterTheRoom(GameObject go)
    {
        isRoom = true;

        pos_door = go_door.transform.position;
        doorOpen = go.transform.GetChild(0).gameObject;

        StartCoroutine(DoorAction(true, doorOpen, () =>
        {
            SetRoom(true);
        }));
    }

    public void SetRoom(bool on)
    {
        if (on)
        {
            if (go_room != null)
            {
                go_room.SetActive(true);
            }
            else
            {
                go_room = Instantiate(roomInit[OpeningListener.ROOMNUM]);

                roomLight = go_room.GetComponentInChildren<Light>();
            }

            go_room.transform.position = go_door.transform.position;
            go_room.transform.eulerAngles = go_door.transform.eulerAngles;

            SetAllPlanesActive(false);
        }
        else
        {
            StartCoroutine(DoorAction(false, doorOpen, () =>
            {
                go_room.SetActive(false);

                isRoom = false;

                SetAllPlanesActive(true);
            }));
        }
    }

    IEnumerator DoorAction(bool on, GameObject go, Action done)
    {
        float start = on ? 0f : -175f;
        float end = on ? -175f : 0f;

        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime;
            float turn = Mathf.Lerp(start, end, time);
            go.transform.localEulerAngles = new Vector3(0, turn, 0);

            yield return new WaitForEndOfFrame();
        }

        done();
    }

    private void CreateCube(Vector3 point, Vector3 groundRot)
    {
        go_door.transform.position = point;
        go_door.transform.localScale = Vector3.one;

        go_door.transform.LookAt(Camera.main.transform.position);
        Vector3 goRot = go_door.transform.eulerAngles;

        go_door.transform.eulerAngles = new Vector3(groundRot.x, goRot.y, goRot.z);
    }


    private void SetAllPlanesActive(bool value)
    {
        Base.Instance.ARPlaneManager.enabled = false;

        foreach (var plane in Base.Instance.ARPlaneManager.trackables)
            plane.gameObject.SetActive(value);
    }
}