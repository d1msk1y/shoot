using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{

    public PlayerController playerController;
    public Shooting shooting;
    public Text totalCoinsTxTShop;

    public void StartGame()
    {
        playerController.enabled = true;
        shooting.enabled = true;
        GameManager.instance.StartCoroutine(GameManager.instance.lowPasFrequencyFadeUp());
    }

    public void ExitGame()
    {
        playerController.enabled = false;
        shooting.enabled = false;
    }

    private void Update()
    {
        totalCoinsTxTShop.text = "COINS:" + ScoreSystem.instance.totalCoins;
    }

}
