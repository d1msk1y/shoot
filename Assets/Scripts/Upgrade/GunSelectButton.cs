using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunSelectButton : MonoBehaviour
{
    public Gun currentGun;
    public int gunIndex;
    public static GunSelectButton instance;
    private Text buttonText;
    private Button thisButton;
    public UpgradeButton upgradeButton;

    private void Awake()
    {
        buttonText = GetComponentInChildren<Text>();
        thisButton = GetComponent<Button>();
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        if(gunIndex == UpgradeSystem.instance.gunIndex)
        {
            currentGun = upgradeButton.gunStages[upgradeButton.lvlStage];
            buttonText.text = "SELECTED";
            thisButton.interactable = false;
        }
        else if(gunIndex != UpgradeSystem.instance.gunIndex)
        {
            buttonText.text = "SELECT";
            thisButton.interactable = true;
        }

    }

    public void Select()
    {
        PlayerPrefs.SetInt("gunIndex", gunIndex);
    }
}
