using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{

    [SerializeField] GameObject Ghost;
    [SerializeField] GameObject Pants;
    GameObject instance;
    SpriteRenderer spriteRenderer;
    [SerializeField] GameObject[] ears;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //Restart
            instance = Instantiate(Ghost);
            instance.transform.position = transform.position;
            spriteRenderer.enabled = false;
            Destroy(ears[0]);
            Destroy(ears[1]);
            //instance = Instantiate(Pants);
            //instance.transform.position = transform.position;
            StartCoroutine(waitToProceed());
            //Ghost
        }
    }

    private IEnumerator waitToProceed()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
