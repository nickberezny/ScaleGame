using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorWin : MonoBehaviour
{
    [SerializeField] string nextSceneName;
    PlayerMotor temp;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.transform.tag);
        if(collision.gameObject.TryGetComponent<PlayerMotor>(out temp))
        {
            Debug.Log("WIN!!!");
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
