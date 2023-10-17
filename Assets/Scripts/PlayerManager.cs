using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerManager : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] KeyCode right;
    [SerializeField] KeyCode left;
    [SerializeField] KeyCode up;
    [SerializeField] KeyCode lightAttack;
    [SerializeField] KeyCode heavyAttack;

    [Header("Body")]
    [SerializeField] Transform middle;
    [SerializeField] string facing;
    [SerializeField] string previousFacing;

    [Header("World")]
    [SerializeField] float speed = 5f;
    [SerializeField] float castDist = 1f;
    [SerializeField] float jumpPower = 2f;
    [SerializeField] float gravScale = 5f;
    [SerializeField] float gravFall = 40f;

    [Header("Score Management")]
    public float health;
    public float wins;
    public bool dead;
    public bool heavyAttacking;
    public bool lightAttacking;
    public bool touchingOpponent;

    [Header("Outsider Components")]
    public PlayerManager opponent;

    [Header("Animation Control")]
    public Animator animator;
    public float attackAnimCountMax = 12f;
    public string playerName;

    #region private vars
    float horizontalMove;
    float heavyAttackDamage = 2f;
    float lightAttackDamage = 1f;
    float attackAnimCount;

    bool grounded = false;
    bool jump = false;
    bool stopMoveLeft;
    bool stopMoveRight; 
    
    Rigidbody2D myBody;
    #endregion 
  
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>(); //assigns rigid body to this variable
        attackAnimCount = attackAnimCountMax;
    }

    void Update() //use update for things that need an instant response
    {
        #region input
        if (Input.GetKey(left) && !stopMoveLeft)
        {
            horizontalMove = -1;
            animator.SetBool("running", true);
        } else if (Input.GetKey(right) && !stopMoveRight)
        {
            horizontalMove = 1;
            animator.SetBool("running", true);
        } else
        {
            horizontalMove = 0;
            animator.SetBool("running", false);
        }

        if (Input.GetKeyDown(up) && grounded && (!stopMoveLeft || !stopMoveRight))
        {
            animator.SetTrigger("Jump");
            jump = true;
        }

        if (Input.GetKeyDown(heavyAttack)) { animator.SetTrigger("Attack"); } 
        
       //if (Input.GetKeyDown(lightAttack) && attackAnimCount == attackAnimCountMax) { lightAttacking = true; }
        #endregion


      
    }

    void FixedUpdate() //use fixed update for things that shouldn't fluxuate 
    {
        #region movement
        float moveSpeed = horizontalMove * speed;

        #region jump

        if (jump)
        {
            myBody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            jump = false;
        }

        if (myBody.velocity.y >= 0f) //if the rigid body is going up
        {
            myBody.gravityScale = gravScale; //go up with this gravity scale
        }
        else if (myBody.velocity.y <= 0f) //if it is going down
        {
            myBody.gravityScale = gravFall; //fall with this gravity scale
        }

        RaycastHit2D hit = Physics2D.Raycast(middle.position, Vector2.down, castDist); //holds the information from a raycast hit 

        Debug.DrawRay(middle.position, Vector2.down * castDist, Color.blue);

        if (hit.collider != null && hit.transform.tag == "Ground")
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        #endregion 

        myBody.velocity = new Vector3(moveSpeed, myBody.velocity.y, 0f);
        #endregion

        #region attack animation
       
        #endregion
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        #region barrierEnter
        if (collision.gameObject.tag == "BarrierLeft")
        {
            stopMoveLeft = true;
        }
        if (collision.gameObject.tag == "BarrierRight")
        {
            stopMoveRight = true;
        }
        #endregion
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        #region barrierExit
        if (collision.gameObject.tag == "BarrierLeft")
        {
            stopMoveLeft = false;
        }
        if (collision.gameObject.tag == "BarrierRight")
        {
            stopMoveRight = false;
        }
        #endregion
    }
}
