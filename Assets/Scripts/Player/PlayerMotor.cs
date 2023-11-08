using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{

    Animator anim;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    float dt = 0;
    [SerializeField] float velocity;

    bool isFalling = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        dt = Time.deltaTime;
        Debug.Log(Mathf.Abs(rb.velocity.y));

        if (Mathf.Abs(rb.velocity.y) > 0.2f)
        {
            anim.SetBool("isFalling", true);
            isFalling = true;
            Debug.Log("TRUE!");
        }
        else
        {
            anim.SetBool("isFalling", false);
            isFalling = false;
            Debug.Log("FALSE!");
        }

        if (Input.GetKey(KeyCode.RightArrow) && !isFalling)
        {
            anim.SetBool("isWalking", true);
            transform.position += new Vector3(velocity*dt, 0, 0);
            spriteRenderer.flipX = false;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && !isFalling)
        {
            anim.SetBool("isWalking", true);
            transform.position -= new Vector3(velocity*dt, 0, 0);
            spriteRenderer.flipX = true;
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        
    }

}
