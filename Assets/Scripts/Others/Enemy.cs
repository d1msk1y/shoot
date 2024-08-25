using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Enemy : MonoBehaviour
{
    public ParticleSystem enemyExplosion;
    public int score;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Bullet" && gameObject.tag == "Enemy")
        {
            EnemyDie();
            CameraShaker.Instance.ShakeOnce(8f, 8f, 0.2f, .2f);
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int isBerserk = PlayerPrefs.GetInt("isBoughtBerserk");
        int isBerserkP = PlayerPrefs.GetInt("isBoughtBerserkP");
        if(collision.tag == "Player" && isBerserk == 1 && gameObject.tag == "Enemy")
        {
            EnemyDie();
            PlayerController.instance.rb.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
            CameraShaker.Instance.ShakeOnce(8f, 8f, 0.1f, .1f);
        }
        
        if(gameObject.tag == "LavaEnemy" && collision.tag == "Player" && isBerserkP == 1)
        {
            EnemyDie();
            PlayerController.instance.rb.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
            CameraShaker.Instance.ShakeOnce(8f, 8f, 0.1f, .1f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "LvlSpawner" && PlayerController.instance.isAlive)
        {
            Destroy(gameObject);
        }
    }

    public void EnemyDie()
    {
        //---Die section---//
        SoundManager.instance.audioSource.PlayOneShot(SoundManager.instance.playerDieClip);
        PlayerController.instance.ResetEssentials();
        Instantiate(enemyExplosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
        //---Die section---//

        //---Score section--//

        if(PlayerController.instance.isAlive)
            ScoreSystem.instance.AddScore(score);

        //---Score section--//
    }
}
