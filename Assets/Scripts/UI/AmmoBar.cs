using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoBar : MonoBehaviour
{
    public Image bar;
    public float amount;

    private void Update()
    {
        amount = Shooting.instance.currentAmmo * 0.01f;

        bar.fillAmount = amount;
    }

}
