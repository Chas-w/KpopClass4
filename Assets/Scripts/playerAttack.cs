using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    [SerializeField] player2Health player2Health;
    [SerializeField] int damage;
    //[SerializeField] Collision2D player1Collision;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player2" && Input.GetButtonDown("Attack"))
        {
            //Debug.Log("touching");
            player2Health.TakeDamage(damage);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player2" && Input.GetButtonDown("Attack"))
        {
            //Debug.Log("touching");
            player2Health.TakeDamage(damage);
        }
    }
    // Update is called once per fra
    void Update()
    {
        //Debug.Log(attacking);
    }
}
