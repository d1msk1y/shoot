using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    public string isBoughtKey;
    public int isBought;
    private Button thisButton;
    public int cost;
    public Text gunCostTXT, lvlStageTXT;

    private void Awake()
    {
        isBought = PlayerPrefs.GetInt(isBoughtKey);
        thisButton = GetComponent<Button>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(isBought == 1)
        {
            thisButton.gameObject.SetActive(false);
            lvlStageTXT.gameObject.SetActive(true);
            gunCostTXT.gameObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        gunCostTXT.text = cost + "$";

        if (ScoreSystem.instance.totalCoins < cost)
        {
            thisButton.interactable = false;
        }
        if (ScoreSystem.instance.totalCoins >= cost)
        {
            thisButton.interactable = true;
        }
        if (ScoreSystem.instance.totalCoins < cost)
        {
            thisButton.interactable = false;
        }
    }

    public void Buy()
    {
        ScoreSystem.instance.totalCoins = ScoreSystem.instance.totalCoins -= cost;
        thisButton.gameObject.SetActive(false);
        gunCostTXT.gameObject.SetActive(false);
        lvlStageTXT.gameObject.SetActive(true);
        isBought = 1;
        PlayerPrefs.SetInt(isBoughtKey, isBought);
        PlayerPrefs.SetInt("Coins", ScoreSystem.instance.totalCoins);
    }

}
