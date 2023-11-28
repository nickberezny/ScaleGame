using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseBox : MonoBehaviour
{

    [SerializeField] Box boxParent;
    GameManager gm;
    AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void SetGM(GameManager manager)
    {
        gm = manager;
    }

    private void Update()
    {
        transform.position = boxParent.transform.position - new Vector3(0, 0, 1.0f);
        transform.localScale = boxParent.transform.localScale;

        if (boxParent.index == gm.activeBox)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(new Vector2(mousePos.x, mousePos.y), new Vector3(0, 0),1f);
                if (hit)
                {
                    Debug.Log(hit.transform.tag);
                    if (hit.transform.transform.parent.gameObject != boxParent.transform.parent.gameObject)
                    {
                        gm.deactivate(boxParent.index);
                    }
                }
                else
                {
                    Debug.Log("Nada");
                    gm.deactivate(boxParent.index);
                }
            }
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("Click!");
        // boxParent.changeBoxStatus(!boxParent.pausePhysics);
        if (boxParent.pausePhysics) gm.deactivate(boxParent.index);
        else if (!boxParent.pausePhysics)
        {
            source.Play();
            gm.requestActive(boxParent.index);
        }
    }



}
