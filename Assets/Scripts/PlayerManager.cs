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
    [SerializeField] Transform playerView;
    [SerializeField] Transform middle;
    [SerializeField] string facing;
    [SerializeField] string previousFacing;
    public float knockbackForce;
    public float knockbackCounter;
    public float knockbackTotalTime;
    public bool knockFromRight;
    

    [Header("World")]
    [SerializeField] float speed = 5f;
    [SerializeField] float castDist = 1f;
    [SerializeField] float jumpPower = 2f;
    [SerializeField] float gravScale = 5f;
    [SerializeField] float gravFall = 40f;

    [Header("Score Management")]
    public float healthMax;
    public float healthCurrent;
    public float wins;
    public float attackRange = .5f;
    public bool dead;
    public bool heavyAttacking;
    public bool lightAttacking;
    public bool touchingOpponent;

    [Header("Outsider Components")]
    public PlayerManager opponent;
    public Transform opponentPosition;
    public string opponentTag;
    public bool opponentColliding;

    [Header("Animation Control")]
    public Animator animator;
   // public string playerName;

    #region private vars
    float horizontalMove;
    int heavyAttackDamage = 2;
    int lightAttackDamage = 1;
   

    bool grounded = false;
    bool jump = false;
    bool stopMoveLeft;
    bool stopMoveRight;
    
    Rigidbody2D myBody;
    #endregion 
  
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>(); //assigns rigid body to this variable
        healthCurrent = healthMax;
        
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

        if (Input.GetKeyDown(heavyAttack)) 
        {
            animator.SetTrigger("Attack");
            if (opponent.opponentColliding)
            {
                heavyAttacking = true;
                opponent.knockbackCounter = opponent.knockbackTotalTime;
                opponent.healthCurrent -= heavyAttackDamage;
            }
            else { heavyAttacking = false; }
        } else { heavyAttacking = false;  }


        //if (Input.GetKeyDown(lightAttack) && attackAnimCount == attackAnimCountMax) { lightAttacking = true; }
        #endregion

        #region sprite direction
        if (horizontalMove > 0f)
        {
            playerView.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (horizontalMove < 0f)
        {
            playerView.localScale = new Vector3(-1f, 1f, 1f);
        }
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

        #region knock back
        if (knockbackCounter < 0 && opponent.heavyAttacking == false) { myBody.velocity = new Vector3(moveSpeed, myBody.velocity.y, 0f); }
        else 
        { 
            if (knockFromRight)
            {
                myBody.velocity = new Vector3(-knockbackForce, 3f, 0f);
            } 
            if (!knockFromRight)
            {
                myBody.velocity = new Vector3(knockbackForce, 3f, 0f);
            }
            knockbackCounter -= Time.deltaTime;
        }
        #endregion 

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

        #region opponent collisions
        if (collision.gameObject.tag == opponentTag)
        {
            opponentColliding = true;
            if (collision.transform.position.x <= transform.position.x) { opponent.knockFromRight = true; }
            if (collision.transform.position.x > transform.position.x) { opponent.knockFromRight = false; }
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

        #region opponent collisions
        if (collision.gameObject.tag == opponentTag)
        {
            opponentColliding = false;
        }
        #endregion
    }

}
