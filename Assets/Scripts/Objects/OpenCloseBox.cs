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
