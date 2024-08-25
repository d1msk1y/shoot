using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{

    public int bestScore, currentScore, scoreModifier = 1, maxScoreModifier;
    public Text scoreTXT, highScoreTXT, scoreModifierTXT;

    public int totalCoins, currentCoins;
    public Text receivedCoinsTXT,totalCoinsTXT;

    private float modifierLifetime;
    public float maxModifierLifitime;
    public Animator scoreModifierAnimator;

    public int currentKills, totalKills;
    public Text totalKillsTXT, currentKillsTXT;
    public Text deathTXT;

    public static ScoreSystem instance;


    private void Awake()
    {
        instance = this;
        if (PlayerPrefs.HasKey("BestScore"))
        {
            bestScore = PlayerPrefs.GetInt("BestScore");
        }
        if (PlayerPrefs.HasKey("Coins"))
        {
            totalCoins = PlayerPrefs.GetInt("Coins");
        }
        if (PlayerPrefs.HasKey("TotalKills"))
        {
            totalKills = PlayerPrefs.GetInt("TotalKills");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentScore = 0;        
        scoreTXT.text = currentScore.ToString("SCORE: 0");
    }
    public void AddScore(int Addedscore)
    {
        currentScore += Addedscore * scoreModifier;
        modifierLifetime = maxModifierLifitime;
        scoreModifierAnimator.Play("Bump");
        if (modifierLifetime > 0 && scoreModifier < maxScoreModifier)
        {
            scoreModifier += 1;
        }
        currentKills += 1;
    }

    private void Update()
    {
        int isBoughtMaxModifier = PlayerPrefs.GetInt("isBoughtMaxCombo");
        if(isBoughtMaxModifier == 1)
        {
            maxScoreModifier = 8;
        }

        if(modifierLifetime >= 0)
        {
            modifierLifetime -= 1 * Time.deltaTime;
        }
        if(modifierLifetime <= 0)
        {
            scoreModifier = 1;
        }

        scoreModifierTXT.text = ("X" + scoreModifier);
        scoreTXT.text = currentScore.ToString("SCORE: 0"); 
        
        totalKillsTXT.text = "TOTAL KILLS: " + totalKills;

        currentKillsTXT.text = "KILLS: " + currentKills;

        deathTXT.text = "DEATHS: " + PlayerController.instance.deaths;
    }

    public void SumScore()
    {
        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            PlayerPrefs.SetInt("BestScore", bestScore);
        }

        totalKills = totalKills += currentKills;
        PlayerPrefs.SetInt("TotalKills", totalKills);

        currentCoins = currentScore / 10;
        totalCoins += currentCoins;
        PlayerPrefs.SetInt("Coins", totalCoins);

        //---TXT section---//
        scoreTXT.text = currentScore.ToString("SCORE: 0");
        highScoreTXT.text = bestScore.ToString("HIGHSCORE: 0");
        receivedCoinsTXT.text = currentCoins.ToString("RECEIVED COINS: 0");
        totalCoinsTXT.text = totalCoins.ToString("TOTAL COINS: 0");
        //---TXT section---//
    }
}