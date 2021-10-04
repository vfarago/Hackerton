using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ObjectFix : MonoBehaviour
{
    [SerializeField] private GameObject worldSpaceObject;

    private ARCameraManager m_CameraManager;
    CameraFacingDirection m_CurrentCameraFacingDirection;

    private void Awake()
    {
        m_CameraManager = Camera.main.GetComponent<ARCameraManager>();
        m_CurrentCameraFacingDirection = m_CameraManager.currentFacingDirection;
    }

    // Update is called once per frame
    void Update()
    {
        var updatedCameraFacingDirection = m_CameraManager.currentFacingDirection;
        if (updatedCameraFacingDirection.Equals(CameraFacingDirection.World))
        {
            m_CurrentCameraFacingDirection = updatedCameraFacingDirection;
            Application.onBeforeRender += OnBeforeRender;

            print(updatedCameraFacingDirection);
        }
    }

    void OnBeforeRender()
    {
        var camera = Camera.main;
        if (camera && worldSpaceObject)
        {
            worldSpaceObject.transform.position = camera.transform.position + camera.transform.forward;
        }
    }

    public void LookCam()
    {
        var camera = Camera.main;
        if (camera && worldSpaceObject)
        {
            worldSpaceObject.transform.LookAt(Camera.main.transform.position);
            Vector3 rot = worldSpaceObject.transform.eulerAngles;

            worldSpaceObject.transform.eulerAngles = new Vector3(0, rot.y - 180, 0);
        }
    }
}
