using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using EZCameraShake;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]public Rigidbody2D rb;
    public ParticleSystem dieExplosion,coinExplosion;
    public static PlayerController instance;
    private SoundManager soundInstance;
    [HideInInspector]public bool isAlive;
    public float health = 1f;
    [HideInInspector]public int deaths;


    private void Awake()
    {
        isAlive = true;
        soundInstance = SoundManager.instance;
        instance = this;
        rb = GetComponent<Rigidbody2D>();

        if (PlayerPrefs.HasKey("Deaths"))
        {
            deaths = PlayerPrefs.GetInt("Deaths");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //---Anyway---//

        if (Input.GetKeyDown(KeyCode.Mouse4))
        {
            Shooting.instance.StartCoroutine(Shooting.instance.MiniGun());
        }

        int isMaxHealthIncrease = PlayerPrefs.GetInt("isBoughtMaxHealth");
        float healthMax = 0.2f;
        if (isMaxHealthIncrease == 1)
            healthMax = 0.1f;
        if (health >= 0)
        {
            health -= healthMax * Time.deltaTime;
        }

        if(health <= 0)
        {
            Die();
        }
        //---Anyway---//

        //---Only if pointer under UI---// 
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetKey(KeyCode.Mouse0) || Input.GetButton("Fire1"))
        {
            Shooting.instance.isShooting = true;
            Shooting.instance.Shoot(rb);//Recoil for player
        }
        if (Input.GetKeyUp(KeyCode.Mouse0) || Input.GetButtonUp("Fire1"))
        {
            StartCoroutine(RechardgeDelay(1f));
        }
        //---Only if pointer under UI---// 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "DieZone")
        {
            Die();
        }
        if(collision.collider.tag == "Enemy" || collision.collider.tag == "LavaEnemy")
        {
            Die();
            Shooting.instance.currentAmmo = Shooting.instance.ammo;
        }

        if(collision.collider.tag == "Coin")
        {
            ScoreSystem.instance.AddScore(1000);
            Destroy(collision.gameObject);
            rb.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
            soundInstance.audioSource.PlayOneShot(soundInstance.coinHit, 1f);
            Instantiate(coinExplosion,collision.transform.position , Quaternion.identity);
        }

        if(collision.collider.tag == "Ground")
        {
            soundInstance.audioSource.PlayOneShot(soundInstance.groundHit, 1f);
        }

    }

    public void ResetEssentials()
    {
        health = 1f;
        Shooting.instance.currentAmmo = 100f;
    }

    public void Die()
    {
        deaths += 1;
        PlayerPrefs.SetInt("Deaths", deaths);

        GameManager.instance.lowPasFrequencyFadeOut();
        CameraShaker.Instance.ShakeOnce(8f, 10f, 0.3f, 0.4f);
        CameraMovement.instance.transform.position = new Vector3(transform.position.x,transform.position.y, -20);
        GameManager.instance.ShowRestart();        
        isAlive = false;
        soundInstance.audioSource.PlayOneShot(soundInstance.playerDieClip, 1f);
        Instantiate(dieExplosion, transform.position,Quaternion.identity);
        gameObject.SetActive(false);
        ScoreSystem.instance.SumScore();
        health = 0;
    }

    IEnumerator RechardgeDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Shooting.instance.isShooting = false;
    }
}
