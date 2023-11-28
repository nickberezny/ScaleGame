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
    [SerializeField] GameObject[] earObjs;
    [SerializeField] AudioClip walkingClip;
    [SerializeField] AudioClip fallingClip;
    bool isFalling = false;
    bool isWalking = false;
    public bool controllable = true;
    AudioSource source;
    

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        dt = Time.deltaTime;
        //Debug.Log(Mathf.Abs(rb.velocity.y));

        if(Input.GetKey(KeyCode.P))
        {
            for (int i = 0; i < earObjs.Length; i++)
            {
                Destroy(earObjs[i].GetComponent<Joint2D>());
                Destroy(earObjs[i].GetComponent<EarSpring>());
                earObjs[i].transform.parent = null;

            }
        }

        if (Mathf.Abs(rb.velocity.y) > 0.3f)
        {
            anim.SetBool("isFalling", true);
            isFalling = true;

        }
        else
        {
            anim.SetBool("isFalling", false);
            isFalling = false;
            //Debug.Log("FALSE!");
        }

        anim.SetBool("isWalking", false);
        isWalking = false;

        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && !isFalling && controllable)
        {
            if (!checkColl(1))
            {
                anim.SetBool("isWalking", true);
                transform.position += new Vector3(velocity * dt, 0, 0);
                spriteRenderer.flipX = false;
                isWalking = true;
            }
            
        }
        else if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))  && !isFalling && controllable)
        {
            if (!checkColl(-1))
            {
                anim.SetBool("isWalking", true);
                transform.position -= new Vector3(velocity * dt, 0, 0);
                spriteRenderer.flipX = true;
                isWalking = true;
            }
        }

        if (isWalking && (!source.isPlaying || source.clip == fallingClip))
        {
            source.clip = walkingClip;
            source.Play();
        }
        else if(isFalling && (!source.isPlaying || source.clip == walkingClip))
        {
            source.clip = fallingClip;
            source.Play();
        }
        if (!isWalking && !isFalling && source.isPlaying) source.Stop();


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
