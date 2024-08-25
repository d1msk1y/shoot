using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public ParticleSystem coinExplosion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" || collision.collider.tag == "Bullet")
        {
            PickUp();
        }
    }

    void PickUp()
    {
        ScoreSystem.instance.AddScore(1000);
        Destroy(gameObject);
        SoundManager.instance.audioSource.PlayOneShot(SoundManager.instance.coinHit, 1f);
        PlayerController.instance.ResetEssentials();
        Instantiate(coinExplosion, transform.position, Quaternion.identity);
    }

}
