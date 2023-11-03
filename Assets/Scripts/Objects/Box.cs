using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    GameManager gm;
    public int index;

    public List<int> collisionIndexes = new List<int>();

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

    public void SetGameManager(GameManager manager)
    {
        gm = manager;
    }

    public void changeBoxStatus(bool status)
    {
        if(status)
        {
            if (gm.requestActive(index)) pausePhysics = true;
        }
        else
        {
            gm.deactivate(index);
            pausePhysics = false;
        }
    }

    Vector2 MoveCenter(Vector2 d)
    {
        float dx = d.x;
        float dy = d.y;
        float xn = 0.0f;
        float yn = 0.0f;

        if (dx > 0)
        {
            xn = dx + transform.position.x + transform.localScale.x / 2.0f;
            xn = CheckCollision(new Vector2(1.0f, 0.0f), dx, xn);
            if (xn > transform.position.x + transform.localScale.x / 2.0f)
            {
                transform.position = new Vector3(xn- transform.localScale.x / 2.0f, transform.position.y, 0);
            }
            xn = transform.position.x - transform.localScale.x / 2.0f;
        }
        if (dx < 0)
        {
            xn = dx + transform.position.x - transform.localScale.x / 2.0f;
            xn = CheckCollision(new Vector2(-1.0f, 0.0f), -dx, xn);
            if (xn < transform.position.x - transform.localScale.x / 2.0f)
            {
                transform.position = new Vector3(xn + transform.localScale.x / 2.0f, transform.position.y, 0);
            }
            xn = transform.position.x + transform.localScale.x / 2.0f;
        }

        if (dy > 0)
        {
            yn = dy + transform.position.y + transform.localScale.y / 2.0f;
            yn = CheckCollision(new Vector2(0.0f, 1.0f), dy, yn);
            if (yn > transform.position.y + transform.localScale.y / 2.0f)
            {
                transform.position = new Vector3(transform.position.x, yn - transform.localScale.y / 2.0f, 0);
            }
            yn = transform.position.y - transform.localScale.y / 2.0f;
        }
        if (dy < 0)
        {
            yn = dy + transform.position.y - transform.localScale.y / 2.0f;
            yn = CheckCollision(new Vector2(0.0f, -1.0f), -dy, yn);
            if (yn < transform.position.y - transform.localScale.y / 2.0f)
            {
                transform.position = new Vector3(transform.position.x, yn + transform.localScale.y / 2.0f, 0);
            }
            yn = transform.position.y + transform.localScale.y / 2.0f;
        }

        collisionIndexes = new List<int>();

        return new Vector2(xn, yn);

    }

    public bool SetScaleAndCenter(float xn0, float xn1, float yn0, float yn1)
    {

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

        collisionIndexes = new List<int>();

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
                    if (dir.y != 0)
                    {
                        float margin_dir = (hits[i].transform.position.y - hits[i].point.y) / Mathf.Abs((hits[i].transform.position.y - hits[i].point.y));
                        return hits[i].point.y - margin_dir * margin;
                    }
                    break;
                case "box":
                    collisionIndexes.Add(index);
                    Box b = hits[i].transform.GetComponent<Box>();
                    b.collisionIndexes = collisionIndexes;
                    if (!collisionIndexes.Contains(b.index))
                    {
                        Vector2 newPoint = b.MoveCenter(new Vector2(dir.x*(distance+margin), dir.y * (distance + margin)));
                        if (dir.x != 0)
                        {
                            float margin_dir = (hits[i].transform.position.x - hits[i].point.x) / Mathf.Abs((hits[i].transform.position.x - hits[i].point.x));
                            return newPoint.x - margin_dir * margin;
                        }

                        if (dir.y != 0)
                        {
                            float margin_dir = (hits[i].transform.position.y - hits[i].point.y) / Mathf.Abs((hits[i].transform.position.y - hits[i].point.y));
                            return newPoint.y - margin_dir * margin;
                        }
                    }
                    

                    break;
                   
            }
        }

        return old_val;

    }




}
