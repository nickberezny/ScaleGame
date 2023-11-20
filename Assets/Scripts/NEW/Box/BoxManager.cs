using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : MonoBehaviour
{
    public float x0, x1, y0, y1, xc, yc;
    public int index;
    public bool pausePhysics = false;
    [SerializeField] Collider2D castColl;
    GameManager gm;
    SpriteRenderer renderer;

    public List<int> collisionIndexes = new List<int>();

    void Awake()
    {
        castColl.enabled = false;
        renderer = GetComponent<SpriteRenderer>();

        xc = transform.position.x;
        yc = transform.position.y;

    }

    private void Update()
    {

    }


    public float MoveCenter(Vector2 dir, float distance)
    {
        float newDistance = CheckCollsion(dir, distance);
        float dx = dir.x * newDistance;
        float dy = dir.y * newDistance;
        xc += dx;
        x0 += dx;
        x1 += dx;

        yc += dy;
        y0 += dy;
        y1 += dy;

        return newDistance;
    }

    public float ScaleBox(Vector2 side, float newPos)
    {

        //if (dir == -1) newAmount = CheckCollsion(side, amount);

        if (side.x > 0) x1 = newPos;
        if (side.x < 0) x0 = newPos;
        if (side.y > 0) y1 = newPos;
        if (side.y < 0) y0 = newPos;

        transform.localScale = new Vector3(Mathf.Abs(x1 - x0), Mathf.Abs(y1 - y0), 0.0f);
        transform.position = new Vector3(xc + (Mathf.Abs(x1) - Mathf.Abs(x0)) , yc + (Mathf.Abs(y1) - Mathf.Abs(y0)) / 2.0f, 0.0f);

        return 0;
    }

    private float CheckCollsion(Vector2 dir, float distance)
    {

        RaycastHit2D[] hits = new RaycastHit2D[10];
        castColl.enabled = true;
        int count = castColl.Cast(dir, hits, distance);
        castColl.enabled = false;
        float newDistance = distance;

        for (int i = 0; i < count; i++)
        {

            switch (hits[i].transform.tag)
            {
                case "Box":
                    collisionIndexes.Add(index);
                    BoxManager box = hits[i].transform.GetComponent<BoxManager>();
                    box.collisionIndexes = collisionIndexes;
                    if (!collisionIndexes.Contains(box.index))
                    {
                        newDistance = box.MoveCenter(dir, newDistance);
                        return newDistance;
                    }
                    break;

            }
        }
        return newDistance;
    }

    public void SetGameManager(GameManager manager)
    {
        gm = manager;
    }

    public void changeBoxStatus(bool status)
    {
        if (status)
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



}
