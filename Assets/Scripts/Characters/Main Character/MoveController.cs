using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public SpriteRenderer sr;

    void Start() {
        rb = GetComponent<Rigidbody2D>();    
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update() {
        Walk();
        Reflect();
        Jump();
        CheckingGround();
        Attack();
    }

    public Vector2 moveVector;
    public float speed = 6f;
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

    public float jumpForce = 20f;

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
