using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player2Attack : MonoBehaviour
{
    [SerializeField] playerHealth player1Health;
    [SerializeField] int damage;
    //[SerializeField] Collision2D player1Collision;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && Input.GetButtonDown("Attack2"))
        {
            //Debug.Log("touching");
            player1Health.TakeDamage(damage);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && Input.GetButtonDown("Attack2"))
        {
            //Debug.Log("touching");
            player1Health.TakeDamage(damage);
        }
    }
    // Update is called once per fra
    void Update()
    { 
        //Debug.Log(attacking);
    }
    
}
