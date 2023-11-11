using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Worm : MonoBehaviour
{
    [SerializeField] float speed;
    float dt = 0;
    float dir = 1;
    [SerializeField] Collider2D castColl;

    PlayerMotor motor;
    SpriteRenderer renderer;


    private void Awake()
    {
        castColl.enabled = false;
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        dt = Time.deltaTime;
        CheckCollisionAndGround(dt);
        transform.position += new Vector3(dir * speed * dt, 0, 0);
        renderer.flipX = dir<0;
    }

    void CheckCollisionAndGround(float dt)
    {
        RaycastHit2D[] hits = new RaycastHit2D[10];
        castColl.enabled = true;
        int count = castColl.Cast(new Vector2(dir,0), hits, 1.05f*speed*dt);
        castColl.enabled = false;

        for (int i = 0; i <count; i++)
        {
            if (hits[i].transform.TryGetComponent<PlayerMotor>(out motor))
            {
                //kill play
                SceneManager.LoadScene("Level4");
                break;
            }   
            if (hits[i].transform.tag == "box" || hits[i].transform.tag == "ground")
            {
                dir = -dir;
                break;
            }

        }

        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x + transform.localScale.x / 2.0f + dir * 1.05f * speed * dt, transform.position.y), new Vector2(0, -1), 1.05f * transform.localScale.y / 2.0f);
        RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(transform.position.x + transform.localScale.x / 2.0f + dir * 110.0f * speed * dt, transform.position.y), new Vector2(0, -1), 1.05f * transform.localScale.y / 2.0f);
        if (!hit && !hit2) dir = - dir;

    }

    //check collision & if ground -> change dir
    //move
}
