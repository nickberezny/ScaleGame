using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{

    float xc, yc;
    public float x0, x1, y0, y1;
    float margin = 0.025f;
    Collider2D coll;
    Rigidbody2D rb;
    public bool pausePhysics = false;

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        x0 = transform.position.x - transform.localScale.x / 2.0f;
        x1 = transform.position.x + transform.localScale.x / 2.0f;
        y0 = transform.position.y - transform.localScale.y / 2.0f;
        y1 = transform.position.y + transform.localScale.y / 2.0f;

        xc = transform.position.x;
        yc = transform.position.y;

        Debug.Log(x0 + "," + x1 + "," + y0 + "," + y1);
    }

    private void Update()
    {
        if (pausePhysics) rb.Sleep();
        if (!pausePhysics && rb.IsSleeping()) rb.WakeUp();

        x0 = transform.position.x - transform.localScale.x / 2.0f;
        x1 = transform.position.x + transform.localScale.x / 2.0f;
        y0 = transform.position.y - transform.localScale.y / 2.0f;
        y1 = transform.position.y + transform.localScale.y / 2.0f;

        if(!pausePhysics)
        {
            xc = transform.position.x;
            yc = transform.position.y;
        }
        
    }


    public void MoveCenter(float dx, float dy)
    {
        if (dx != 0)
        {
            //if (CheckCollision(new Vector2(dx / Mathf.Abs(dx), 0.0f))) return;
        }
        if (dy != 0)
        {
            //if (CheckCollision(new Vector2(0.0f, dy / Mathf.Abs(dy)))) return;
        }

        transform.position += new Vector3(dx, dy, 0.0f);
        return;

    }

    public bool SetScaleAndCenter(float xn0, float xn1, float yn0, float yn1)
    {

        Debug.Log("Y first: " + yc + "," + yn1);

        if (xn0-x0<0)
        {
            xn0 = (CheckCollision(new Vector2((xn0 - x0) / Mathf.Abs(xn0 - x0), 0.0f), Mathf.Abs(xn0 - x0), xn0));
        }
        if (xn1-x1>0)
        {
            xn1 = (CheckCollision(new Vector2((xn1-x1)/Mathf.Abs(xn1-x1), 0.0f), Mathf.Abs(xn1 - x1), xn1));
        }
        if (yn0-y0<0)
        {
            yn0 = (CheckCollision(new Vector2(0.0f, (yn0 - y0) / Mathf.Abs(yn0 - y0)), Mathf.Abs(yn0 - y0), yn0));
        }
        if (yn1-y1>0)
        {
            yn1 = (CheckCollision(new Vector2(0.0f, (yn1 - y1) / Mathf.Abs(yn1 - y1)), Mathf.Abs(yn1 - y1), yn1));
        }

        Debug.Log("Y second: " + yc + "," + yn1);

        if (xn0 > xc) xn0 = xc;
        if (xn1 < xc) xn1 = xc;
        if (yn0 > yc) yn0 = yc;
        if (yn1 < yc) yn1 = yc;

        transform.localScale = new Vector3(Mathf.Abs(xn1-xn0), Mathf.Abs(yn1-yn0), 0.0f);
        transform.position = new Vector3((xn0+xn1) / 2.0f, (yn0+yn1) / 2.0f, 0.0f);

        x0 = xn0;
        x1 = xn1;
        y0 = yn0;
        y1 = yn1;

        Debug.Log("Y: " + yc + "," + yn1);
        
        return true;

    }

    float CheckCollision(Vector2 dir, float distance, float old_val)
    {
        RaycastHit2D[] hits = new RaycastHit2D[10];
        int count = coll.Cast(dir, hits, distance);
        
        for(int i = 0; i < count; i++)
        { 
            switch (hits[i].transform.tag)
            {
                case "ground":
                    if (dir.x != 0)
                    {
                        float margin_dir = (hits[i].transform.position.x - hits[i].point.x) / Mathf.Abs((hits[i].transform.position.x - hits[i].point.x));
                        return hits[i].point.x - margin_dir * margin;
                    }
                    if (dir.y != 0) return hits[i].point.y - dir.y * margin; 
                    break;
                case "player":
                    //try moving player
                    //or kill
                    break;
                   
            }
        }

        return old_val;

    }

    private void OnMouseDown()
    {
        Debug.Log("Click!");
        pausePhysics = !pausePhysics;

        
    }


}
