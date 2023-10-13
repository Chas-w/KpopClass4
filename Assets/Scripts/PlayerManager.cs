using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{
    public float speed = 5f;
    public float castDist = 1f;
    public float jumpPower = 2f;
    public float gravScale = 5f;
    public float gravFall = 40f;

    float horizontalMove;

    bool grounded = false;
    bool jump = false;

    Rigidbody2D myBody;

    void Start()
    {
        myBody = GetComponent<Rigidbody2D>(); //assigns rigid body to this variable
    }

    void Update() //use update for things that need an instant response
    {
        horizontalMove = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded)
        {
            jump = true;
        }
    }

    void FixedUpdate() //use fixed update for things that shouldn't fluxuate 
    {
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
