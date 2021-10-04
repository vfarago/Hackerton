using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [SerializeField]
    private GameObject door;

    public virtual void CrateObject(string name)
    {        
        Debug.Log("부모");
        switch(name)
        {
            default: break;

            case "Door":
                Instantiate(door);
                door.AddComponent<Door>();
                break;
        }
    }

    public virtual void DestroyObject()
    {
        Debug.Log("부모");
    }

}
