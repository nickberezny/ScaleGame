using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    [SerializeField] Vector2 dir;
    [SerializeField] float strength;
    Collider2D coll;
    RaycastHit2D[] hits;

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
    }

    private void Update()
    {
        hits = new RaycastHit2D[10];
        int count = coll.Cast(new Vector2(0, 0), hits, 0);

        for (int i = 0; i < count; i++)
        {
            Debug.Log(hits[i].transform.tag);
            if (hits[i].transform.tag == "box") 
                hits[i].transform.GetComponent<Rigidbody2D>().AddForce(strength * dir);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("coll!!!");
    }
}