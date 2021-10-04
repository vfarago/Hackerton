using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyPivotAction : MonoBehaviour
{
    private Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        Vector3 pos = Vector3.zero;
        if (Input.GetMouseButtonDown(0))
        {
            pos = Input.mousePosition;
        }
        if (Input.touchCount > 0)
        {
            pos = Input.GetTouch(0).position;
        }

        if (pos != Vector3.zero)
        {
            RaycastHit hit;
            Ray actRay = mainCam.ScreenPointToRay(pos);


            if (Physics.Raycast(actRay, out hit))
            {
                bool checkHit = false;
                int actionIndex = 0;

                if (hit.collider.GetComponent<MeshCollider>() != null)
                {
                    if (!hit.collider.GetComponent<MeshCollider>().isTrigger)
                    {
                        checkHit = true;
                        actionIndex = 1;
                    }
                }
                if (hit.collider.GetComponent<SphereCollider>() != null)
                {
                    if (!hit.collider.GetComponent<SphereCollider>().isTrigger)
                    {
                        checkHit = true;
                        actionIndex = 2;
                    }
                }
                if (hit.collider.GetComponent<CapsuleCollider>() != null)
                {
                    if (!hit.collider.GetComponent<CapsuleCollider>().isTrigger)
                    {
                        checkHit = true;
                        actionIndex = 3;
                    }
                }
                if (hit.collider.GetComponent<BoxCollider>() != null)
                {
                    if (!hit.collider.GetComponent<BoxCollider>().isTrigger)
                    {
                        checkHit = true;
                        actionIndex = 4;
                    }
                }

                if (checkHit)
                {
                    switch (actionIndex)
                    {
                        case 1:
                            break;

                        case 2:
                            Vector3 cent = hit.collider.bounds.center;
                            Vector3 pot = hit.point;

                            hit.collider.GetComponent<Rigidbody>().AddForce((cent - pot) * 1000, ForceMode.Acceleration);
                            break;

                        case 3:
                            break;

                        case 4:
                            break;
                    }
                }
            }
        }
    }
}
