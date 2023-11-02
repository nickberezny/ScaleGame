using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRayCast : MonoBehaviour
{
    Collider2D coll;
    float distance = 0.5f;
    bool finished = false;

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
    }


    void Update()
    {
        if(!finished)
        {
            RaycastHit2D[] hits = new RaycastHit2D[10];
            int count = coll.Raycast(new Vector2(-1, 0), hits, distance);

            for (int i = 0; i < count; i++)
            {
                Debug.Log(hits[i].transform.tag + "," + count);
                switch (hits[i].transform.tag)
                {
                    case "ground":
                        Debug.Log("Contact at " + distance);
                        finished = true;
                        break;

                }
            }
        }

        distance += 0.01f;


    }
}
