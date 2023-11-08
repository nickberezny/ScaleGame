using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteControl : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField] Box boxParent;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        transform.position = boxParent.transform.position;
        spriteRenderer.size = new Vector2(boxParent.transform.localScale.x, boxParent.transform.localScale.y);
    }
}
