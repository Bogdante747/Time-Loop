using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boar : MonoBehaviour
{
    GameObject player;
     public Rigidbody2D rb;
    public Animator anim;
    public SpriteRenderer sr;
 
    public Vector2 pos;
    public float speed = 6f;
    public bool faceRight = false;
 
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player");
    }
 
    void Update()
    {
        Walk();
        CheckingGround();
        Reflect();
        if (Input.GetMouseButtonDown(0)) {
            onAttack = true;
            StartCoroutine(AttackTimer());
        }
    }

    // public float direction;
    // void Run()
    // {
    //     direction = player.transform.position.x - transform.position.x;
 
    //     if (Mathf.Abs(direction) < 20)
    //     {
    //         anim.SetFloat("moveX", Mathf.Abs(direction));
    //         pos = transform.position;
    //         pos.x += Mathf.Sign(direction) * speed * Time.deltaTime;
    //         transform.position = pos;
    //     }
    // }

    public float moveRight = -1f;
    public Transform DirectionCheck;
    public float checkRadius = 0.5f;
    public LayerMask Ground;
    void Walk() {
        bool direction = Physics2D.OverlapCircle(DirectionCheck.position, checkRadius, Ground);
        anim.SetFloat("moveX", Mathf.Abs(moveRight));
        if (direction == true && moveRight == -1f) {
            moveRight = 1f;
        } else if (direction == true && moveRight == 1f) {
            moveRight = -1f;
        } 
        GetComponent<Rigidbody2D>().velocity = new Vector2 ( speed * moveRight, GetComponent<Rigidbody2D>().velocity.y);
    }

    public bool onGround;
    public Transform GroundCheck;

    void CheckingGround() {
        onGround = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, Ground);
    }

    void Reflect() {
        if ((moveRight > 0 && !faceRight) || (moveRight < 0 && faceRight)){
            transform.localScale *= new Vector2 (-1, 1);
            faceRight = !faceRight;
        }
    }

    public int health = 20;
    public float invulnerabilityTime = 0.5f;
    public bool onAttack = false;
    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.CompareTag("Attack") && onAttack == true){
            anim.SetBool("hit", true);
            speed = 0f;
            health -= 10;
            onAttack = false;
            if (health <= 0){ 
                Destroy(gameObject);
            }
            StartCoroutine(InvulnerabilityTimer());
        }
    }   
    IEnumerator AttackTimer(){
        yield return new WaitForSeconds(0.8f);
        onAttack = false;    
    }
    IEnumerator InvulnerabilityTimer(){
        yield return new WaitForSeconds(invulnerabilityTime);
        anim.SetBool("hit", false);
        speed = 6f;
    }
}
