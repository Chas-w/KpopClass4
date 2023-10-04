using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class player2Control : MonoBehaviour
{
    [SerializeField] Transform player1Position;
    [SerializeField] playerAttack p1Attack;

    public float speed = 5f;
    public float castDist = 1f;
    public float jumpPower = 2f;
    public float gravScale = 5f;
    public float gravFall = 40f;
 
    float horizontalMove;
    float bounceSpeed = 10f;
    float knockBack = 50f;

    bool grounded = false;
    bool jump = false;
    bool dirRight;
    //bool hit;
    Animator myAnim;
    Rigidbody2D myBody;
    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>(); //assigns rigid body to this variable
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(p2_attacking);
        #region movement
        horizontalMove = Input.GetAxis("Horizontal2");

        if (Input.GetButtonDown("Jump2") && grounded)
        {
            jump = true;
            myAnim.SetBool("jumping", true);
            // SOUND CODE HERE
        }

        if (horizontalMove > 0.1f || horizontalMove < -0.1f)
        {
            myAnim.SetBool("running", true);
            // SOUND CODE HERE
        }
        else
        {
            myAnim.SetBool("running", false);
        }

        if (horizontalMove > 0f)
        {
            dirRight = true;
            transform.localScale = new Vector3(.2f, .2f, .2f);
        }
        else if (horizontalMove < 0f)
        {
            dirRight = false;
            transform.localScale = new Vector3(-.2f, .2f, .2f);
        }
        if (horizontalMove == 0f)
        {
            if (player1Position.position.x > transform.position.x)
            {
                dirRight = true;
                transform.localScale = new Vector3(.2f, .2f, .2f);
            }
            else if (player1Position.position.x < transform.position.x)
            {
                dirRight = false;
                transform.localScale = new Vector3(-.2f, .2f, .2f);
            }
        }
        #endregion

        if (p1Attack.attacked == true)
        {
            myBody.AddForce(Vector2.up * bounceSpeed, ForceMode2D.Impulse);
            if (dirRight)
            {
                myBody.velocity = new Vector3(-knockBack, myBody.velocity.y, 0f);
            }
            if (!dirRight)
            {
                myBody.velocity = new Vector3(knockBack, myBody.velocity.y, 0f);
            }

            p1Attack.attacked = false;
        }

        if (Input.GetButton("Attack2"))
        {
            myAnim.SetBool("attacking", true);
            // SOUND CODE HERE
        }
        if (Input.GetButtonUp("Attack2"))
        {
            myAnim.SetBool("attacking", false);
        }
        if (Input.GetButton("Block2"))
        {
            myAnim.SetBool("blocking", true);
            // SOUND CODE HERE
        }
        if (Input.GetButtonUp("Block2"))
        {
            myAnim.SetBool("blocking", false);
        }
    }

    void FixedUpdate() //use fixed update for things that shouldn't fluxuate 
    {
        
        #region movement
        float moveSpeed = horizontalMove * speed;

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

        #endregion

        myBody.velocity = new Vector3(moveSpeed, myBody.velocity.y, 0f);
        
    }

}
