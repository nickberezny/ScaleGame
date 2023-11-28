using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubble : MonoBehaviour
{
    [SerializeField] RectTransform[] bubbles;
    [SerializeField] RectTransform rules;

    [SerializeField] AudioClip[] clips;

    AudioSource source;
    int currentActive = -1;

    int popBubbleCounter = 0;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        for (int i = 0; i < bubbles.Length; i++)
        {
            bubbles[i].gameObject.SetActive(false);
        }
    }
    public void ActivateRandomBubble()
    {
        int i = currentActive;
        while(i == currentActive) i = Random.Range(0, bubbles.Length);
        Debug.Log(i);
        if(currentActive > -1) bubbles[currentActive].gameObject.SetActive(false);
        bubbles[i].gameObject.SetActive(true);
        currentActive = i;
        source.clip = clips[i];
        source.Play();
        popBubbleCounter = 15;
        StopCoroutine(waitToPop());
        StartCoroutine(waitToPop());
    }

    public void toggleRules()
    {
        rules.gameObject.SetActive(!rules.gameObject.activeSelf);
    }

    private IEnumerator waitToPop()
    {
        while(popBubbleCounter > 0)
        {
            popBubbleCounter -= 1;
            yield return new WaitForSeconds(0.2f);
        }

        bubbles[currentActive].gameObject.SetActive(false);
        currentActive = -1;
    }

}
