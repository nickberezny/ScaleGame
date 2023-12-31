﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] boxObjects;
    private Box[] boxes;

    private OpenCloseBox openClose;

    public int activeBox;

    private void Awake()
    {
        boxes = new Box[boxObjects.Length];
        for(int i = 0; i < boxObjects.Length; i++)
        {
            boxes[i] = boxObjects[i].GetComponentInChildren<Box>();
            boxes[i].SetGameManager(this);
            try { boxes[i].transform.parent.GetComponentInChildren<OpenCloseBox>().SetGM(this); }
            catch { }
            boxes[i].pausePhysics = false;
            boxes[i].index = i;
        }

        activeBox = -1;
    }

    private void Update()
    {
       
    }

    public bool requestActive(int index)
    {
        StartCoroutine(SetActiveNext(index));
        return true;
    }

    public void deactivate(int index)
    {
        boxes[index].changeBoxStatus(false);
        if (activeBox == index) activeBox = -1;
    }

    IEnumerator SetActiveNext(int index)
    {
        yield return new WaitForEndOfFrame();
        if (activeBox >= 0)
        {
            boxes[activeBox].changeBoxStatus(false);
        }
        boxes[index].changeBoxStatus(true);
        activeBox = index;
        
    }

}
