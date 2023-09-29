using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player2Attack : MonoBehaviour
{
    [SerializeField] playerHealth player1Health;
    [SerializeField] int damage;

    float knockBack = 5f;
    float bounce = 10f;

    bool touching;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            touching = true;
            //player2Health.TakeDamage(damage);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log("touching");
            player1Health.TakeDamage(damage);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            touching = false;
        }
    }
    // Update is called once per fra
    void Update()
    {
        if (touching && Input.GetButtonDown("Attack2"))
        {
            player1Health.TakeDamage(damage);
        }
    }

}
