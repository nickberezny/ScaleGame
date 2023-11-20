using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{

    [SerializeField] Vector2 localScaleTransformer = new Vector2(1,1);
    [SerializeField] Collider2D castColl;
    [SerializeField] bool fixedInPlace = false;
    [SerializeField] Transform bolt;

    GameManager gm;
    public int index;

    public List<int> collisionIndexes = new List<int>();

    float xc, yc;
    public float x0, x1, y0, y1;
    float margin = 0.0f;
    Collider2D coll;
    Rigidbody2D rb;
    SpriteRenderer renderer;
    public bool pausePhysics = false;

    private void Awake()
    {
        castColl.enabled = false;
        coll = GetComponent<Collider2D>();
        renderer = GetComponent<SpriteRenderer>();
        if (!TryGetComponent<Rigidbody2D>(out rb))
        {
            rb = GetComponentInParent<Rigidbody2D>();
        }
        x0 = transform.position.x - localScaleTransformer.x*transform.localScale.x / 2.0f;
        x1 = transform.position.x + localScaleTransformer.x*transform.localScale.x / 2.0f;
        y0 = transform.position.y - localScaleTransformer.y*transform.localScale.y / 2.0f;
        y1 = transform.position.y + localScaleTransformer.y*transform.localScale.y / 2.0f;

        if(!bolt)
        {
            xc = transform.position.x;
            yc = transform.position.y;
        }
        else
        {
            xc = bolt.transform.position.x;
            yc = bolt.transform.position.y;
        }
        

        Debug.Log(x0 + "," + x1 + "," + y0 + "," + y1);
    }

    private void Update()
    {



        if (pausePhysics)
        {
            rb.gravityScale = 0.0f;
            rb.Sleep();
        }
        if (!pausePhysics && rb.IsSleeping())
        {
            rb.gravityScale = 1.0f;
            rb.WakeUp();
        }


        x0 = transform.position.x - localScaleTransformer.x * transform.localScale.x / 2.0f;
        x1 = transform.position.x + localScaleTransformer.x * transform.localScale.x / 2.0f;
        y0 = transform.position.y - localScaleTransformer.y * transform.localScale.y / 2.0f;
        y1 = transform.position.y + localScaleTransformer.y * transform.localScale.y / 2.0f;

        if(!pausePhysics && !fixedInPlace)
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
            //if (gm.requestActive(index)) pausePhysics = true;
            pausePhysics = true;
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0.5f);
        }
        else
        {
            //gm.deactivate(index);
            pausePhysics = false;
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 255f);
        }
    }

    Vector2 MoveCenter(Vector2 d)
    {

        if (fixedInPlace) return new Vector2(0, 0);

        float dx = d.x;
        float dy = d.y;
        float xn = 0.0f;
        float yn = 0.0f;

        if (dx > 0)
        {
            
            xn = dx + transform.position.x + localScaleTransformer.x * transform.localScale.x / 2.0f;
            xn = CheckCollision(new Vector2(1.0f, 0.0f), dx, xn);
            if (xn > transform.position.x + localScaleTransformer.x * transform.localScale.x / 2.0f)
            {
                transform.position = new Vector3(xn - localScaleTransformer.x * transform.localScale.x / 2.0f, transform.position.y, 0);
            }
            xn = transform.position.x - localScaleTransformer.x * transform.localScale.x / 2.0f;
        }
        if (dx < 0)
        {
            xn = dx + transform.position.x - localScaleTransformer.x * transform.localScale.x / 2.0f;
            xn = CheckCollision(new Vector2(-1.0f, 0.0f), -dx, xn);
            if (xn < transform.position.x - localScaleTransformer.x * transform.localScale.x / 2.0f)
            {
                transform.position = new Vector3(xn + localScaleTransformer.x * transform.localScale.x / 2.0f, transform.position.y, 0);
            }
            xn = transform.position.x + localScaleTransformer.x * transform.localScale.x / 2.0f;
        }

        if (dy > 0)
        {
            yn = dy + transform.position.y + localScaleTransformer.y * transform.localScale.y / 2.0f;
            yn = CheckCollision(new Vector2(0.0f, 1.0f), dy, yn);
            if (yn > transform.position.y + localScaleTransformer.y * transform.localScale.y / 2.0f)
            {
                transform.position = new Vector3(transform.position.x, yn - localScaleTransformer.y * transform.localScale.y / 2.0f, 0);
            }
            yn = transform.position.y - localScaleTransformer.y * transform.localScale.y / 2.0f;
        }
        if (dy < 0)
        {
            yn = dy + transform.position.y - localScaleTransformer.y * transform.localScale.y / 2.0f;
            yn = CheckCollision(new Vector2(0.0f, -1.0f), -dy, yn);
            if (yn < transform.position.y - localScaleTransformer.y * transform.localScale.y / 2.0f)
            {
                transform.position = new Vector3(transform.position.x, yn + localScaleTransformer.y * transform.localScale.y / 2.0f, 0);
            }
            yn = transform.position.y + localScaleTransformer.y * transform.localScale.y / 2.0f;
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

        if(fixedInPlace)
        {
            if (xn0 > xc) xn0 = xc;
            if (xn1 < xc) xn1 = xc;
            if (yn0 > yc) yn0 = yc;
            if (yn1 < yc) yn1 = yc; 
        }


        if (xn0 > x1 - 0.2f) xn0 = x1 - 0.2f;
        if (xn1 < x0 + 0.2f) xn1 = x0 + 0.2f;
        if (yn0 > y1 - 0.2f) yn0 = y1 - 0.2f;
        if (yn1 < y0 + 0.2f) yn1 = y0 + 0.2f; 


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
        castColl.enabled = true;
        int count = castColl.Cast(dir, hits, distance);
        castColl.enabled = false;

        for (int i = 0; i < count; i++)
        {

            //if (dir.x > 0 && hits[i].collider.bounds.extents.x + hits[i].collider.bounds.center.x <= x0 + 0.01f) break;
            //if (dir.x < 0 && -hits[i].collider.bounds.extents.x + hits[i].collider.bounds.center.x >= x1 - 0.01f) break;
            //if (dir.y > 0 && hits[i].collider.bounds.extents.y + hits[i].collider.bounds.center.y <= y0 + 0.01f) break;
            //if (dir.y < 0 && -hits[i].collider.bounds.extents.y + hits[i].collider.bounds.center.y >= y1 - 0.01f) break;
            Box b;

            switch (hits[i].transform.tag)
            {
                case "ground":
                    if (dir.x != 0)
                    {
                        float x_hit = 0;
                        if (dir.x < 0) x_hit = hits[i].collider.bounds.extents.x + hits[i].collider.bounds.center.x;
                        if (dir.x > 0) x_hit = -hits[i].collider.bounds.extents.x + hits[i].collider.bounds.center.x;
                        return x_hit;
                    }
                    if (dir.y != 0)
                    {
                        float y_hit = 0;
                        if(dir.y < 0) y_hit = hits[i].collider.bounds.extents.y + hits[i].collider.bounds.center.y;
                        if (dir.y > 0) y_hit = -hits[i].collider.bounds.extents.y + hits[i].collider.bounds.center.y;
                        return y_hit; 
                    }
                    break;
                case "box":
                    collisionIndexes.Add(index);
                    b = hits[i].transform.GetComponent<Box>();
                    b.collisionIndexes = collisionIndexes;
                    if (!collisionIndexes.Contains(b.index))
                    {
                        Debug.Log(hits[i].point.x);
                        Vector2 newPoint = b.MoveCenter(new Vector2(dir.x*(distance+margin), dir.y * (distance + margin)));
                        if (dir.x != 0)
                        {
                            float x_hit = 0;
                            if (dir.x < 0) x_hit = hits[i].collider.bounds.extents.x + hits[i].collider.bounds.center.x;
                            if (dir.x > 0) x_hit = -hits[i].collider.bounds.extents.x + hits[i].collider.bounds.center.x;
                            return x_hit;
                        }

                        if (dir.y != 0)
                        {
                            float y_hit = 0;
                            if (dir.y < 0) y_hit = hits[i].collider.bounds.extents.y + hits[i].collider.bounds.center.y;
                            if (dir.y > 0) y_hit = -hits[i].collider.bounds.extents.y + hits[i].collider.bounds.center.y;
                            return y_hit;
                        }
                    }
                    break;
                case "Worm":
                    Debug.Log("WORM!" + dir.x + "," + dir.y);
                    if (dir.y != 0)
                    {
                        collisionIndexes.Add(index);
                        b = hits[i].transform.GetComponent<Box>();
                        b.collisionIndexes = collisionIndexes;
                        Vector2 newPoint = b.MoveCenter(new Vector2(dir.x * (distance + margin), dir.y * (distance + margin)));
                        float y_hit = 0;
                        if (dir.y < 0) y_hit = hits[i].collider.bounds.extents.y + hits[i].collider.bounds.center.y;
                        if (dir.y > 0) y_hit = -hits[i].collider.bounds.extents.y + hits[i].collider.bounds.center.y;
                        Debug.Log("y_hit" + y_hit);
                        return y_hit;

                        
                    }
                    if (dir.x < 0) return hits[i].collider.bounds.extents.x + hits[i].collider.bounds.center.x;
                    if (dir.x > 0) return -hits[i].collider.bounds.extents.x + hits[i].collider.bounds.center.x;

                    break;

                   
            }
        }

        return old_val;

    }




}
