using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    private float myTime;
    // Start is called before the first frame update
    void Start()
    {
        myTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        myTime += Time.deltaTime;
        if (myTime >= 2.0f) Destroy(gameObject);
    }
}
