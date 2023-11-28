using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    [SerializeField] Vector2 dir;
    [SerializeField] float strength;
    Collider2D coll;
    RaycastHit2D[] hits;
    bool hitPlayer = false;
    bool hitPlayerLastTime = false;

    public GameObject boxParent;

    AudioSource source;

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        hits = new RaycastHit2D[10];
        int count = coll.Cast(new Vector2(0, 0), hits, 0);

        if (!hitPlayer) source.Stop();
        hitPlayer = false;

        for (int i = 0; i < count; i++)
        {
            if ((hits[i].transform.tag == "box" || hits[i].transform.tag == "Flag") && hits[i].transform.gameObject != boxParent)
            {

                hits[i].transform.GetComponent<Rigidbody2D>().AddForce(strength * dir);
                if(hits[i].transform.name == "Bun")
                {
                    hitPlayer = true;
                    if (!hitPlayerLastTime)
                    {
                        hitPlayerLastTime = true;
                        source.Play();
                    }
                }
            }
                

        }

        if (!hitPlayer) hitPlayerLastTime = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("coll!!!");
    }
}