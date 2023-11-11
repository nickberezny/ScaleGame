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
    [SerializeField] Collider2D collCast;

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
        //Debug.Log(Mathf.Abs(rb.velocity.y));

        if (Mathf.Abs(rb.velocity.y) > 0.2f)
        {
            anim.SetBool("isFalling", true);
            isFalling = true;
            //Debug.Log("TRUE!");
        }
        else
        {
            anim.SetBool("isFalling", false);
            isFalling = false;
            //Debug.Log("FALSE!");
        }

        anim.SetBool("isWalking", false);

        if (Input.GetKey(KeyCode.RightArrow) && !isFalling)
        {
            if (!checkColl(1))
            {
                anim.SetBool("isWalking", true);
                transform.position += new Vector3(velocity * dt, 0, 0);
                spriteRenderer.flipX = false;
            }
            
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && !isFalling)
        {
            if (!checkColl(-1))
            {
                anim.SetBool("isWalking", true);
                transform.position -= new Vector3(velocity * dt, 0, 0);
                spriteRenderer.flipX = true;
            }
        }
        
    }

    bool checkColl(int dir)
    {
        collCast.enabled = true;
        RaycastHit2D[] hits = new RaycastHit2D[10];
        int count = collCast.Cast(new Vector2(dir, 0), hits, velocity * dt + 0.05f, true);
        //Debug.Log(hits.Length);

        for (int i = 0; i < count; i++)
        {
            if (hits[i].transform.gameObject != gameObject && (hits[i].transform.tag == "ground" || hits[i].transform.tag == "box"))
            {
                collCast.enabled = false;
                Debug.Log("Coll!");
                return true;
            }
        }

        collCast.enabled = false;
        return false;
    }

}
