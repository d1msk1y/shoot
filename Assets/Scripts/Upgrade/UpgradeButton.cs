using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpgradeButton : MonoBehaviour
{
    public Gun[] gunStages;
    public int lvlStage;
    public string lvlStageKey;
    public int gunIndex;
    [HideInInspector]public Button thisButton;
    public GunSelectButton selectButton;
    public static UpgradeButton instance;
    public Text costTXT, upgradeStage;

    private void Awake()
    {
        instance = this;
        gunIndex = PlayerPrefs.GetInt("gunIndex");
        lvlStage = PlayerPrefs.GetInt(lvlStageKey);
        thisButton = GetComponent<Button>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(lvlStage <= 3)
            costTXT.text = gunStages[lvlStage + 1].upgradeCost + "$";
        gunIndex = PlayerPrefs.GetInt("gunIndex");
        lvlStage = PlayerPrefs.GetInt(lvlStageKey);

        if(lvlStage > 0)
            upgradeStage.text = lvlStage + 1 + "/5";
        if(lvlStage < 1)
            upgradeStage.text = 1 + "/5";

        if (lvlStage > 3)
        {
            thisButton.interactable = false;
            costTXT.gameObject.SetActive(false);
        }


        if (UpgradeSystem.instance.gunIndex == selectButton.gunIndex)
        {
            if (lvlStage <= 4 && ScoreSystem.instance.totalCoins >= gunStages[lvlStage + 1].upgradeCost)
            {
                thisButton.interactable = true;
            }
        }

        if (ScoreSystem.instance.totalCoins < gunStages[lvlStage + 1].upgradeCost)
        {
            thisButton.interactable = false;
        }
        else if (selectButton.gunIndex != UpgradeSystem.instance.gunIndex)
        {
            thisButton.interactable = false;
        }
    }

    public void Upgrade()
    {
        if (lvlStage < 5 && ScoreSystem.instance.totalCoins >= gunStages[lvlStage].upgradeCost)
        {
            GunSelectButton.instance.currentGun = gunStages[lvlStage];
            if (lvlStage <= 3)
                ScoreSystem.instance.totalCoins -= gunStages[lvlStage + 1].upgradeCost;
            costTXT.text = gunStages[lvlStage].upgradeCost + "$";
            PlayerPrefs.SetInt("Coins", ScoreSystem.instance.totalCoins);

            lvlStage += 1;
            PlayerPrefs.SetInt(lvlStageKey, lvlStage);
        }
        if(lvlStage > 4)
        {
            thisButton.interactable = false;
        }
    }

}
