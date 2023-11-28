using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBox : MonoBehaviour
{
    //set scale to box scale
    [SerializeField] Box boxParent;
    [SerializeField] Vector3 scaleMult;
    Transform childTransform;
    Wind wind;

    private void Awake()
    {
        childTransform = transform.GetChild(0);
        wind = GetComponent<Wind>();
        wind.boxParent = boxParent.gameObject;
    }

    private void Update()
    {
        transform.localScale = Vector3.Scale(scaleMult, boxParent.transform.localScale);
        childTransform.localScale = Vector3.Scale(new Vector3(scaleMult.x, 1.0f, scaleMult.z), boxParent.transform.localScale);
        transform.position = boxParent.transform.position;

    }
}
