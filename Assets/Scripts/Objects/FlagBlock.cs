using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagBlock : MonoBehaviour
{
    Vector3 initPos;
    [SerializeField] Transform followTransform;

    private void Awake()
    {
        initPos = transform.localPosition;
    }


    void Update()
    {
        transform.position = followTransform.position + initPos;
    }
}
