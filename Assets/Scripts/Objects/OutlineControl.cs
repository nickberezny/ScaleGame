using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineControl : MonoBehaviour
{
    Vector3 initPos;
    Vector3 initScale;
    [SerializeField] Box boxParent;

    [SerializeField] bool NW;
    [SerializeField] bool side;

    private void Awake()
    {
        initPos = transform.localPosition;
        initScale = transform.localScale;
    }


    void Update()
    {
        if(NW)
        {
            if(side)
            {
                transform.localScale = new Vector3(transform.localScale.x, boxParent.transform.localScale.y, 1.0f);
                transform.position = new Vector3(boxParent.x0, boxParent.transform.position.y, -0.05f);
            }
            if(!side)
            {
                transform.localScale = new Vector3(boxParent.transform.localScale.x, transform.localScale.y, 1.0f);
                transform.position = new Vector3(boxParent.transform.position.x, boxParent.y1, -0.05f);
            }
        }

        if (!NW)
        {
            if (side)
            {
                transform.localScale = new Vector3(transform.localScale.x, boxParent.transform.localScale.y, 1.0f);
                transform.position = new Vector3(boxParent.x1, boxParent.transform.position.y, -0.05f);
            }
            if (!side)
            {
                transform.localScale = new Vector3(boxParent.transform.localScale.x, transform.localScale.y, 1.0f);
                transform.position = new Vector3(boxParent.transform.position.x, boxParent.y0, -0.05f);
            }
        }
    }
}
