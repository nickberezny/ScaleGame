using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxHandle : MonoBehaviour
{
    [SerializeField] bool dir;
    [SerializeField] bool side;
    [SerializeField] Box BoxParent;

    private Collider2D coll;
    private SpriteRenderer spriteRenderer;

    public bool pausePhysics = false;
    private float center = 0.0f;
    public float z = -1.0f;
    

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        coll.enabled = false;
        spriteRenderer.enabled = false;

        transform.position = new Vector3(transform.position.x, transform.position.y, z);
    }


    private void Update()
    {
        if (pausePhysics != BoxParent.pausePhysics) SetPause(!pausePhysics);

        center = (BoxParent.x1 + BoxParent.x0) / 2.0f;
        if (!side && dir) transform.position = new Vector3(center, BoxParent.y1, z);
        if (!side && !dir) transform.position = new Vector3(center, BoxParent.y0, z);
        center = (BoxParent.y1 + BoxParent.y0) / 2.0f;
        if (side && dir) transform.position = new Vector3(BoxParent.x1, center, z);
        if (side && !dir) transform.position = new Vector3(BoxParent.x0, center, z);
    }

    private void OnMouseDrag()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos= new Vector3(mousePos.x, mousePos.y, 10.0f);
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        float x = mousePos.x;
        float y = mousePos.y;
        bool check = false;

        if(side)
        {
            if (!dir) check = BoxParent.SetScaleAndCenter(x, BoxParent.x1, BoxParent.y0, BoxParent.y1);
            if (dir) check = BoxParent.SetScaleAndCenter(BoxParent.x0, x, BoxParent.y0, BoxParent.y1);

            center = (BoxParent.y1 + BoxParent.y0)/2.0f;

            if (check && dir) transform.position = new Vector3(BoxParent.x1, center, z);
            if (check && !dir) transform.position = new Vector3(BoxParent.x0, center, z);
        }

        if (!side)
        {
            if (!dir) check = BoxParent.SetScaleAndCenter(BoxParent.x0, BoxParent.x1, y, BoxParent.y1);
            if (dir) check = BoxParent.SetScaleAndCenter(BoxParent.x0, BoxParent.x1, BoxParent.y0, y);

            center = (BoxParent.x1 + BoxParent.x0)/2.0f;

            if (check && dir) transform.position = new Vector3(center, BoxParent.y1, z);
            if (check && !dir) transform.position = new Vector3(center, BoxParent.y0, z);
        }

        

       // Debug.Log(x + " " + y);

    }

    private void SetPause(bool val)
    {
        pausePhysics = val;
        coll.enabled = val;
        spriteRenderer.enabled = val;
    }

}
