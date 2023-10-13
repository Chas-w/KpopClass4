using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    #region private vars
    float horizontalMove;

    bool grounded = false;
    bool jump = false;
    bool stopMoveLeft;
    bool stopMoveRight; 

    Rigidbody2D myBody;
    #endregion 

    void Start()
    {
        myBody = GetComponent<Rigidbody2D>(); //assigns rigid body to this variable
    }

    void Update() //use update for things that need an instant response
    {
        #region input
        if (Input.GetKey(left) && !stopMoveLeft)
        {
            horizontalMove = -1; 
        } else if (Input.GetKey(right) && !stopMoveRight)
        {
            horizontalMove = 1; 
        } else
        {
            horizontalMove = 0; 
        }

        if (Input.GetKeyDown(up) && grounded && (!stopMoveLeft || !stopMoveRight))
        {
            jump = true;
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

        myBody.velocity = new Vector3(moveSpeed, myBody.velocity.y, 0f);
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
