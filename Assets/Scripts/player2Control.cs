using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class player2Control : MonoBehaviour
{
    [SerializeField] Transform player1Position;

    public float speed = 5f;
    public float castDist = 1f;
    public float jumpPower = 2f;
    public float gravScale = 5f;
    public float gravFall = 40f;
 
    float horizontalMove;
    float bounceSpeed = 10f;

    bool grounded = false;
    bool jump = false;
    bool dirRight;
    bool hit;

    Rigidbody2D myBody;
    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>(); //assigns rigid body to this variable

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && Input.GetButtonDown("Attack"))
        {
            hit = true;
        }
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
            if (player1Position.position.x > transform.position.x)
            {
                dirRight = true;
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (player1Position.position.x < transform.position.x)
            {
                dirRight = false;
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        #endregion

        if (hit)
        {
            /*
            if (dirRight)
            {
                moveSpeed = -knockBack;
            }
            if (!dirRight)
            {
                moveSpeed = knockBack;
            }
            myBody.velocity = new Vector3(moveSpeed, myBody.velocity.y, 0f);
            */

            myBody.AddForce(Vector2.up * bounceSpeed, ForceMode2D.Impulse);

            hit = false;
        }
    }

    void FixedUpdate() //use fixed update for things that shouldn't fluxuate 
    {
        if (!hit)
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
            }
            else
            {
                grounded = false;
            }

            #endregion

            myBody.velocity = new Vector3(moveSpeed, myBody.velocity.y, 0f);
        }
    }

}
