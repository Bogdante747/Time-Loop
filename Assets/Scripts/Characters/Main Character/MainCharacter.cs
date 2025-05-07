using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCharacter : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public SpriteRenderer sr;
    public float speed = 10f;
    public float jumpForce = 20f;
    public bool gameOver;
    
    

    void Start() {
        rb = GetComponent<Rigidbody2D>();    
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update() {
        gameOver = GameObject.Find("Damage").GetComponent<MainHealth>().gameOver;
        if (gameOver == true) {
            speed = 0f;
            jumpForce = 0f;
            anim.SetBool("death", true);
            if (Input.GetKeyDown(KeyCode.R)) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        } else {

        Reflect();
        Jump();
        Attack();
        }
        Walk();
        CheckingGround();
    }

    public Vector2 moveVector;
    public bool faceRight = true;

    void Walk() {
        moveVector.x = Input.GetAxis("Horizontal");
        anim.SetFloat("moveX", Mathf.Abs(moveVector.x));
        rb.velocity = new Vector2(moveVector.x * speed, rb.velocity.y);
    }

    void Reflect() {
        if ((moveVector.x > 0 && !faceRight) || (moveVector.x < 0 && faceRight)){
            transform.localScale *= new Vector2 (-1, 1);
            faceRight = !faceRight;
        }
    }

    void Jump(){
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && onGround) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    float doubleClickTime = .2f;
    float lastClickTime;
    void Attack() {
        if (Input.GetMouseButtonDown(0)){
            float timeSinceLastClick = Time.time - lastClickTime;
            if (timeSinceLastClick <= doubleClickTime){
                anim.Play("Attack2");
                anim.SetBool("onAttack", true);
            } else {
                anim.Play("Attack1");
                anim.SetBool("onAttack", true);
            }
            lastClickTime = Time.time;
        }
        
    }

    void AttackToggle() {
        anim.SetBool("onAttack", false);
    }

    public bool onGround;
    public Transform GroundCheck;
    public float checkRadius = 0.5f;
    public LayerMask Ground;

    void CheckingGround() {
        onGround = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, Ground);
        anim.SetBool("onGround", onGround);
    }

}
