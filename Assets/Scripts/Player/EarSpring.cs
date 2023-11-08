using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarSpring : MonoBehaviour
{


    private float x, y; 

    private void Awake()
    {
        x = transform.localPosition.x;
        y = transform.localPosition.y;
    }

    void Update()
    {

        if (Vector3.Distance(transform.localPosition, new Vector3(x, y, transform.localPosition.z)) > 0.05f)
        {
            resetPos();
        }
    }

    private void resetPos()
    {
        transform.localPosition = new Vector3(x, y, transform.localPosition.z);
    }
}
