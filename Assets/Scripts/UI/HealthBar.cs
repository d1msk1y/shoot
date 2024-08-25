using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image bar;
    public float amount;

    private void Update()
    {
        amount = PlayerController.instance.health;

        bar.fillAmount = amount;
    }

}
