using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxHandleCorner : MonoBehaviour
{
    [SerializeField] bool right;
    [SerializeField] bool top;
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

        if (!top && right) transform.position = new Vector3(BoxParent.x1, BoxParent.y0, z);
        if (!top && !right) transform.position = new Vector3(BoxParent.x0, BoxParent.y0, z);
        if (top && right) transform.position = new Vector3(BoxParent.x1, BoxParent.y1, z);
        if (top && !right) transform.position = new Vector3(BoxParent.x0, BoxParent.y1, z);
    }

    private void OnMouseDrag()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = new Vector3(mousePos.x, mousePos.y, 10.0f);
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        float x = mousePos.x;
        float y = mousePos.y;
        bool check = false;

        if (top)
        {
            if (!right) check = BoxParent.SetScaleAndCenter(x, BoxParent.x1, BoxParent.y0, y);
            if (right) check = BoxParent.SetScaleAndCenter(BoxParent.x0, x, BoxParent.y0, y);
        }
        if(!top)
        {
            if (!right) check = BoxParent.SetScaleAndCenter(x, BoxParent.x1, y, BoxParent.y1);
            if (right) check = BoxParent.SetScaleAndCenter(BoxParent.x0, x, y, BoxParent.y1);
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
