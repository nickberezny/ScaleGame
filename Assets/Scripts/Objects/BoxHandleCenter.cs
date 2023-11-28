using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxHandleCenter : MonoBehaviour
{
    [SerializeField] Box BoxParent;
    [SerializeField] float clampVal;

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
        transform.position = new Vector3((BoxParent.x1 + BoxParent.x0) / 2.0f, (BoxParent.y1 + BoxParent.y0) / 2.0f, z);

    }

    private void OnMouseDrag()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = new Vector3(mousePos.x, mousePos.y, 10.0f);
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        float dx = Mathf.Clamp(mousePos.x - BoxParent.transform.position.x,-clampVal, clampVal);
        float dy = Mathf.Clamp(mousePos.y - BoxParent.transform.position.y,-clampVal, clampVal);


        BoxParent.MoveCenter(new Vector2(dx,dy));

    }

    private void SetPause(bool val)
    {
        pausePhysics = val;
        coll.enabled = val;
        spriteRenderer.enabled = val;
    }

}
