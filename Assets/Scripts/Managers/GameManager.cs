using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Box[] boxes;

    int activeBox;

    private void Awake()
    {
        for(int i = 0; i < boxes.Length; i++)
        {
            boxes[i].SetGameManager(this);
            boxes[i].pausePhysics = false;
            boxes[i].index = i;
        }

        activeBox = -1;
    }

    public bool requestActive(int index)
    {
        if(activeBox >= 0) boxes[activeBox].pausePhysics = false;
        boxes[index].pausePhysics = true;
        activeBox = index;
        return true;
    }

    public void deactivate(int index)
    {
        boxes[index].pausePhysics = false;
        if (activeBox == index) activeBox = -1;
    }

}
