using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchController : MonoBehaviour
{
    [SerializeField] private GameObject cube;
    [SerializeField] private Text debug;

    private GameObject catchGo;
    private bool isTouch = false;
    private Color[] ranColor = { Color.red, Color.green, Color.yellow, Color.blue, Color.gray, Color.white };

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject(0))
            return;

        //if (Input.touchCount >= 2)
        //{
        //    Ray tRay = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        //    if (Physics.Raycast(tRay, out RaycastHit tHit, 100f))
        //    {
        //        if (tHit.collider.name.Equals(gameObject.name))
        //        {
        //            Vector2 touch0, touch1;
        //            touch0 = Input.GetTouch(0).position;
        //            touch1 = Input.GetTouch(1).position;

        //            if (checkDis.Equals(0))
        //            {
        //                checkDis = Vector2.Distance(touch0, touch1);
        //                return;
        //            }
        //            else
        //            {
        //                float distance = Vector2.Distance(touch0, touch1);

        //                ZoomHandler(distance - checkDis);

        //                checkDis = distance;
        //            }
        //        }
        //    }
        //}
        if (Input.touchCount > 0)
        {
            print("        터치 시작 ");

            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out RaycastHit hit, 1000f))
            {
                if (hit.collider.name.Contains("ARPlane"))
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Began)
                    {
                        if (!isTouch)
                            CreateCube(hit.point);
                    }
                    else if (Input.GetTouch(0).phase == TouchPhase.Moved)
                    {
                        if (isTouch)
                            ChangePosition(hit.point);
                    }
                }
                else
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Began)
                    {
                        catchGo = hit.collider.gameObject;
                        CubeColliderOn(false);
                        isTouch = true;
                    }
                }
            }

            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                CubeColliderOn(true);
                isTouch = false;
            }
        }
    }

    private void CreateCube(Vector3 point)
    {
        catchGo = Instantiate(cube);
        catchGo.transform.position = point;
        catchGo.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        Vector3 v = Camera.main.transform.position - transform.position;
        v.x = v.z = 0;
        catchGo.transform.LookAt(Camera.main.transform.position - v);

        catchGo.GetComponent<Material>().color = ranColor[Random.Range(0, ranColor.Length)];

        float dis = Vector3.Distance(Camera.main.transform.position, point);

        debug.text = string.Format("name: {0} pos:{1} size:{2} \ndistance:{3}",
            catchGo.name, catchGo.transform.position, catchGo.transform.localScale, dis);
    }

    private void CubeColliderOn(bool on)
    {
        if (catchGo == null)
            return;

        catchGo.GetComponent<Collider>().enabled = on;

        if (on)
            catchGo = null;
    }

    private void ChangePosition(Vector3 point)
    {
        catchGo.transform.position = point;
    }
}
