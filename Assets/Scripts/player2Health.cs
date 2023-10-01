using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class player2Health : MonoBehaviour
{
    public int damage;

    public bool blocking; 

    // Start is called before the first frame update
    void Start()
    {
        //health = maxHealth;
    }


    public void TakeDamage(int amount) //amount == how much damage player takes
    {
        damage += amount;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Block2"))
        {
            blocking = true;
        } else if (Input.GetButtonUp("Block2"))
        {
            blocking = false; 
        }
    }
}
