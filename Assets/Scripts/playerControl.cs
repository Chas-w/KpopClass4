using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{
    [SerializeField] Transform player2Position;
    [SerializeField] player2Attack p2Attack;

    public float speed = 5f;
    public float castDist = 1f;
    public float jumpPower = 2f;
    public float gravScale = 5f;
    public float gravFall = 40f;
    public float moveSpeed;

    public bool dirRight;

    float horizontalMove;
    float bounceSpeed = 10f;
    //float knockBack = 5f; 
 
    //float hitCoolDownMax = 5f;
    //float hitCoolDown;

    bool grounded = false;
    bool jump = false;
    //bool hit;

    Rigidbody2D myBody;
    Animator myAnim;

    void Start()
    {
        myBody = GetComponent<Rigidbody2D>(); //assigns rigid body to this variable
        myAnim = GetComponent<Animator>();

        //hitCoolDown = hitCoolDownMax;
    }

    void Update() //use update for things that need an instant response
    {
        #region movement
        horizontalMove = Input.GetAxis("Horizontal"); //is the input

        if (Input.GetButtonDown("Jump") && grounded)
        {
            myAnim.SetBool("jumping", true);
            jump = true; 
        }

        if (horizontalMove > 0.1f || horizontalMove < -0.1f)
        {
            myAnim.SetBool("running", true); 
        } 
        else
        {
            myAnim.SetBool("running", false);
        }

        if (horizontalMove > 0f)
        {
            dirRight = true;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (horizontalMove < 0f)
        {
            dirRight = false;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (horizontalMove == 0f)
        {
            if (player2Position.position.x > transform.position.x)
            {
                dirRight = true;
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (player2Position.position.x < transform.position.x)
            {
                dirRight = false;
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        #endregion

        if (p2Attack.attacked == true)
        {
            myBody.AddForce(Vector2.up * bounceSpeed, ForceMode2D.Impulse);

            p2Attack.attacked = false;
        }
    }

    void FixedUpdate() //use fixed update for things that shouldn't fluxuate 
    {

        #region movement
        moveSpeed = horizontalMove * speed;

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

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, castDist); //holds the information from a raycast hit 

        Debug.DrawRay(transform.position, Vector2.down * castDist, Color.blue);

        if (hit.collider != null && hit.transform.tag == "Ground")
        {
            grounded = true;
            myAnim.SetBool("jumping", false);
        }
        else
        {
            grounded = false;
        }
        myBody.velocity = new Vector3(moveSpeed, myBody.velocity.y, 0f);
        #endregion

    }
}
