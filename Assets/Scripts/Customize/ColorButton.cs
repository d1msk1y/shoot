using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorButton : MonoBehaviour
{
    public string playerPrefsIsBoughtString;
    public int colorIndex;
    public int isBought;//1 = isBought;0= !isBought
    private Button thisButton;

    public static ColorButton instance;

    private void Awake()
    {
        instance = this;
        thisButton = GetComponent<Button>();
    }

    private void Update()
    {
        if(colorIndex == CustomizeSystem.instance.colorIndex)
        {
            thisButton.interactable = false;
        }
        if(colorIndex != CustomizeSystem.instance.colorIndex)
        {
            thisButton.interactable = true;
        }
    }

    public void Select()
    {
        CustomizeSystem.instance.colorButton = this;
    }
}
