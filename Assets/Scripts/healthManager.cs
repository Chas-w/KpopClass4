using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthManager : MonoBehaviour
{
    public Image healthBar2;
    public Image healthBar1;
    public PlayerManager player2;
    public PlayerManager player1;



    private void Start()
    {
        
    }

    private void Update()
    {
        healthBar1.fillAmount = player1.healthCurrent / player1.healthMax;
        healthBar2.fillAmount = player2.healthCurrent/player2.healthMax;
    }
}
