using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuySkillButton : MonoBehaviour
{
    public static BuySkillButton instance;
    public SkillButton selectedSkill;
    public int selectedSkillInt;
    private Button thisButton;
    public Text coinsTXT;


    private void Awake()
    {
        thisButton = GetComponent<Button>();
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        coinsTXT.text = "COINS: " + ScoreSystem.instance.totalCoins;
    }

    public void Buy()
    {
        selectedSkill.Buy();
        CheckSkill();
    }

    public void CheckSkill()
    {
        if (selectedSkill != null || selectedSkill.isBought == 0)
        {
            thisButton.interactable = true;
        }
        if (selectedSkill == null || selectedSkill.isBought == 1 || ScoreSystem.instance.totalCoins < selectedSkill.cost)
        {
            thisButton.interactable = false;
        }
    }

}
