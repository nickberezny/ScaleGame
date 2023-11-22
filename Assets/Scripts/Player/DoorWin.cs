using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorWin : MonoBehaviour
{
    [SerializeField] string nextSceneName;
    [SerializeField] AudioClip winClip;
    AudioManager audioManager;
    SpriteRenderer renderer;
    PlayerMotor temp;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        renderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.transform.tag);
        if(collision.gameObject.TryGetComponent<PlayerMotor>(out temp))
        {
            
            collision.gameObject.GetComponent<Animator>().SetBool("win", true);
            temp.controllable = false;
            audioManager.PlayClip(winClip);
            Debug.Log("WIN!!!");
            StartCoroutine(waitToProceed());
        }
    }

    private IEnumerator waitToProceed()
    {
        yield return new WaitForSeconds(0.1f);
        renderer.enabled = false;
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(nextSceneName);
    }
}
