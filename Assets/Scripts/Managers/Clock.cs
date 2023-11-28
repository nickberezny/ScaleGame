using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    [SerializeField] TextMesh[] textMeshes;
    float min = 0;
    float sec = 0;
    float t0 = 0;
    private void Awake()
    {
        t0 = Time.fixedTime;
        if (FindObjectsOfType<Clock>().Length > 1) Destroy(this.gameObject);
    }


    private void Update()
    {
        min = Mathf.Floor((Time.fixedTime- t0) / 60f);
        sec = Mathf.Round(Time.fixedTime - t0 - min * 60f);

        if (min > 9)
        {
            textMeshes[0].text = Mathf.Floor(min / 10.0f).ToString();
            textMeshes[1].text = (min - 10.0f*Mathf.Floor(min / 10.0f)).ToString();
        }
        else
        {
            textMeshes[1].text = min.ToString();
            textMeshes[0].text = "";
        }

        if(sec > 9)
        {
            textMeshes[2].text = Mathf.Floor(sec / 10.0f).ToString();
            textMeshes[3].text = (sec - 10.0f * Mathf.Floor(sec / 10.0f)).ToString();
        }
        else
        {
            textMeshes[2].text = "0";
            textMeshes[3].text = sec.ToString();
        }
        

    }
}
