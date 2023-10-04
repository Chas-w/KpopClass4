using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class player2Attack : MonoBehaviour
{
    [SerializeField] playerHealth player1Health;
    [SerializeField] int damage;
    public GameObject player1Icon;
    public Sprite p1normalIcon;
    public Sprite p1takeDamageIcon;

    public bool attacked;

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
            touching = true;
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
        if (touching && Input.GetButtonDown("Attack2") && player1Health.blocking == false)
        {
            player1Health.TakeDamage(damage);
            attacked = true;
            player1Icon.GetComponent<UnityEngine.UI.Image>().sprite = p1takeDamageIcon;

        }
    }

}
