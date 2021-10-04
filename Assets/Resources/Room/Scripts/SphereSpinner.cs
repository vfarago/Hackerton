using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSpinner : MonoBehaviour
{
    private void Update()
    {
        gameObject.transform.Rotate(0, 0.5f, 0);
    }
}
