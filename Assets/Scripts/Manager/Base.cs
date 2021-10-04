using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Base : MonoBehaviour
{
    //싱글톤톤
    private static Base instance = null;

    [SerializeField]
    private Material[] adultMaterialArr;
    [SerializeField]
    private Material[] kidMaterialArr;

    [SerializeField]
    private UIManager uiManager;

    [SerializeField]
    private DoorTouch doorTouch;

    [SerializeField]
    private ARPlaneManager arPlaneManager;

    [SerializeField]
    private ObjectManager objectManager;


    private void Awake()
    {
        adultMaterialArr = Resources.LoadAll<Material>("Brosher/Adults");
        kidMaterialArr = Resources.LoadAll<Material>("Brosher/Kids");

        if (null == instance)
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }

        else
            Destroy(this.gameObject);
    }

    public static Base Instance
    {
        get
        {
            if (null == instance)
                return null;

            return instance;
        }
    }

    public UIManager UIManager
    {
        get
        {
            if (null == instance)
                return null;

            return uiManager;
        }
    }

    public DoorTouch DoorTouch
    {
        get 
        {
            if (null == instance)
                return null;

            return doorTouch;
        }
    }

    public ARPlaneManager ARPlaneManager
    {
        get
        {
            if (null == instance)
                return null;

            return arPlaneManager;
        }
    }

    public Material[] AdultMaterialArr
    {
        get
        {
            if (null == instance)
                return null;

            return adultMaterialArr;
        }
    }

    public Material[] KidMaterialArr
    {
        get
        {
            if (null == instance)
                return null;

            return kidMaterialArr;
        }
    }

    public ObjectManager ObjectManager
    {
        get
        {
            if (null == instance)
                return null;

            return objectManager;
        }
    }
}
