using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthControl : MonoBehaviour
{
    public Slider slider;
    // Start is called before the first frame update
    public void SetMaxHealth(int healthValue)
    {
        slider.maxValue = healthValue;
        slider.value = healthValue;
    }
    public void SetHealth(int healthValue)
    {
        slider.value = healthValue;
    }
}
