using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public string scene;

    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")){
            SceneManager.LoadScene(scene);
        }
    }
}
