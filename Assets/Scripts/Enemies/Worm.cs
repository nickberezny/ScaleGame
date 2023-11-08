using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : MonoBehaviour
{
    [SerializeField] float speed;
    float dt = 0;

    // Update is called once per frame
    void Update()
    {
        dt = Time.deltaTime;
        transform.position += new Vector3(speed * dt, 0, 0);
    }
}
