using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallWorm : MonoBehaviour
{
    [SerializeField] Transform p1, p2;
    [SerializeField] float speed;

    Vector3 pos1, pos2;
    float dir = 1;
    float dt = 0;
    float threshold = 1f;
    private void Awake()
    {
        pos1 = p1.position;
        pos2 = p2.position;
    }

    // Update is called once per frame
    void Update()
    {
        dt = Time.deltaTime;
        transform.position += dir * (pos2 - pos1) * speed * dt;
        Debug.Log(Vector3.Distance(transform.position, pos2));
        if(dir == 1 && Vector3.Distance(transform.position, pos2) < threshold)
        {
            dir = -1;
        }
        else if (dir == -1 && Vector3.Distance(transform.position, pos1) < threshold)
        {
            dir = 1;
        }
    }
}
