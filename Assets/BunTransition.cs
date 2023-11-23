using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunTransition : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float threshold;
    [SerializeField] float startPos;

    SpriteRenderer renderer;
    bool move;
    float dt = 0;

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        renderer.enabled = false;
    }

    public void MoveBun()
    {
        move = true;
        renderer.enabled = true;
        transform.position = new Vector3(startPos, transform.position.y, transform.position.z);
    }

    private void Update()
    {
        if(move)
        {
            dt = Time.deltaTime;
            transform.position -= new Vector3(speed * dt, 0, 0);
            if(transform.position.x < threshold)
            {
                renderer.enabled = false;
                move = false;
            }
        }
    }
}
