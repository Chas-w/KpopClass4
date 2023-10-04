using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    [SerializeField] player2Health player2Health;
    [SerializeField] int damage;

    public bool attacked;

    bool touching;
    //[SerializeField] Collision2D player1Collision;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player2")
        {
            touching = true;
            //player2Health.TakeDamage(damage);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player2")
        {
            //Debug.Log("touching");
            touching = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player2")
        {
            touching = false;
        }
    }
    // Update is called once per fra
    void Update()
    {
        if (touching && Input.GetButtonDown("Attack") && player2Health.blocking == false)
        {
            player2Health.TakeDamage(damage);
            attacked = true;
        }
    }
}
