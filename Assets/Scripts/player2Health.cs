using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player2Health : MonoBehaviour
{
    public int health;
    public int maxHealth = 10;
    bool dead;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int amount) //amount == how much damage player takes
    {
        health -= amount;
        if (health <= 0)
        {
            dead = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
