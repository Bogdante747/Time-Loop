using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainHealth : MonoBehaviour
{
    public Text Health;
    public Text GameOver;
    public bool gameOver = false;
    public int health = 100;
    public float invulnerabilityTime = 2f;
    private bool isInvulnerable = false;

    void Update() {
        Health.text = "HP: " + health + "/100";
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.CompareTag("Enemy")){
            if (!isInvulnerable){
                if (health > 0) {
                health -= 10;
                } 
                if (health <= 0){ 
                    GameOver.text = "Game Over\n Нажмите R чтобы перезапустить";
                    gameOver = true;
                }
                isInvulnerable = true;
                StartCoroutine(InvulnerabilityTimer());
            }
        }
    }   
    IEnumerator InvulnerabilityTimer(){
        yield return new WaitForSeconds(invulnerabilityTime);
        isInvulnerable = false;
    }
}
