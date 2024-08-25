using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{

    public Rigidbody2D player;
    public GameObject restartCanvas,gameCanvas,startEnemies;
    public Transform startSpawn;
    public static GameManager instance;
    public Gun pistol, ak47;

    private void Start()
    {
        instance = this;       
    }

    private void Update()
    {
        if(!PlayerController.instance.isAlive && SoundManager.instance.LowPasFilter.cutoffFrequency >= SoundManager.instance.lowPasCutOffModifier)
        {
            lowPasFrequencyFadeOut();
        }     
    }

    public void ShowRestart()
    {
        restartCanvas.SetActive(true);
        gameCanvas.SetActive(false);
    }

    public void Restart()
    {
        //---Score reset---//
        ScoreSystem.instance.currentScore = 0;
        ScoreSystem.instance.currentCoins = 0;
        ScoreSystem.instance.scoreModifier = 0;
        ScoreSystem.instance.currentKills = 0;
        //---Score reset---//

        //---Player reset---//
        PlayerController.instance.gameObject.SetActive(true);
        PlayerController.instance.gameObject.transform.position = startSpawn.position;
        PlayerController.instance.isAlive = true;
        PlayerController.instance.ResetEssentials();
        //---Player reset---//

        Instantiate(startEnemies, startEnemies.transform.position, Quaternion.identity);

        //---Canvas reset---//
        restartCanvas.SetActive(false);
        gameCanvas.SetActive(true);
        //---Canvas reset---//

        StartCoroutine(lowPasFrequencyFadeUp());//Get back a normal sound
        EnemySpawner.instance.ResetEnemySpawner();
    }

    public IEnumerator lowPasFrequencyFadeUp()
    {
        while (SoundManager.instance.LowPasFilter.cutoffFrequency < 22000)
        {
            SoundManager.instance.LowPasFilter.cutoffFrequency += 25000 * Time.deltaTime;
            yield return null;
        }
    }

    public void PrefsResetKeys()
    {
        PlayerPrefs.DeleteAll();
    }

    public void lowPasFrequencyFadeOut()
    {
        SoundManager.instance.LowPasFilter.cutoffFrequency -= SoundManager.instance.lowPasCutOffSpeed * Time.deltaTime;
    }

    #region WeaponChanger
    public void ChangePistol()
    {
        Shooting.instance.gun = pistol;
        Shooting.instance.ammo = Shooting.instance.gun.ammo;
        Shooting.instance.shootFrequency = Shooting.instance.gun.shootFrequency;
        Shooting.instance.recoilForce = Shooting.instance.gun.recoilForce;
    }

    public void ChangeAk47()
    {
        Shooting.instance.gun = ak47;
        Shooting.instance.ammo = Shooting.instance.gun.ammo;
        Shooting.instance.shootFrequency = Shooting.instance.gun.shootFrequency;
        Shooting.instance.recoilForce = Shooting.instance.gun.recoilForce;
    }

    #endregion
}
