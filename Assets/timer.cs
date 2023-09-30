using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class timer : MonoBehaviour
{

    [SerializeField] TMP_Text textComponenet;
    [SerializeField] float startTime;

    float currentTime;
    bool timerStarted = false;

    void Start()
    {
        currentTime = startTime;
        timerStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerStarted)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0)
            {
                Debug.Log("time = 0");
                timerStarted = false;
                currentTime = 0;
            }

            textComponenet.text = currentTime.ToString("f0");

        }

    }
}
