using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    public string isBoughtKey, skillName, about;
    public int isBought;
    public int skillIndex;
    public int cost;
    public Button buyButton;
    public BuySkillButton buyRef;
    private Button thisButton;
    public Text aboutTxt, costTXT;

    private void Awake()
    {
        costTXT = GetComponentInChildren<Text>();
        thisButton = GetComponent<Button>();
        isBought = PlayerPrefs.GetInt(isBoughtKey);
    }

    // Start is called before the first frame update
    void Start()
    {
        CheckIsBought();
    }

    // Update is called once per frame
    void Update()
    {

        if (buyRef.selectedSkillInt == skillIndex)
        {
            thisButton.interactable = false;
        }
        else if (buyRef.selectedSkillInt != skillIndex)
        {
            thisButton.interactable = true;
        }
    }

    public void Select()
    {

        buyRef.selectedSkillInt = skillIndex;

        BuySkillButton.instance.selectedSkill = this;

        aboutTxt.text = skillName + about;
        buyRef.CheckSkill();
    }

    public void Buy()
    {
        if (ScoreSystem.instance.totalCoins < cost)
            return;
        isBought = 1;
        ScoreSystem.instance.totalCoins -= cost;

        CheckIsBought();

        PlayerPrefs.SetInt("Coins", ScoreSystem.instance.totalCoins);
        PlayerPrefs.SetInt(isBoughtKey, isBought);
    }
    void CheckIsBought()
    {
        if (isBought == 0)
            costTXT.text = cost + "$";
        else
            costTXT.text = "SOLD OUT";
    }
}
