using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    public float lifeTime = 2;
    PlayerController player;
    public ParticleSystem defoultExplosion;


    public static Bullet instance;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(lifeTime >= 0)
            lifeTime -= 1 * Time.deltaTime;
        if (lifeTime <= 0)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.tag == "Enemy")
        {
            return;
        }

        SoundManager.instance.audioSource.PlayOneShot(SoundManager.instance.bulletHit, 1f);
        Instantiate(defoultExplosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
