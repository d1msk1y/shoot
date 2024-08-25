using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shooting : MonoBehaviour
{
    //---Gun parameters---//
    [HideInInspector]public float ammo,currentAmmo;//Ammo
    [HideInInspector]public float shootFrequency,currentShootFrequency;//Shooting frequency
    [HideInInspector]public float recoilForce;//Recoil
    //---Gun parameters---//

    private bool isMinigunShoot;

    [HideInInspector]public bool isShooting,isRechardged = true;//Bools

    //---References---/
    public Gun gun;
    public ParticleSystem shootExplosion;
    public GameObject bullet;
    public Transform firePoint;
    public static Shooting instance;
    private SoundManager soundInstance;
    private SpriteRenderer gunSprite;
    //---References---/

    private void Awake()
    {
        gunSprite = GetComponentInChildren<SpriteRenderer>();
        soundInstance = SoundManager.instance;
        instance = this;
    }

    private void Start()
    {
        //---Setting a raferences---//
        gun = UpgradeSystem.instance.selectedGun;
        ammo = gun.ammo;
        currentAmmo = ammo;
        shootFrequency = gun.shootFrequency;
        recoilForce = gun.recoilForce;
        //---Setting a raferences---//
    }

    private void Update()
    {
        if (isMinigunShoot)
            return;
        //---Important variables---//
        Touch touch = Input.GetTouch(0);

        gun = UpgradeSystem.instance.selectedGun;
        gunSprite.sprite = UpgradeSystem.instance.gunSprites[UpgradeSystem.instance.gunIndex];

        //Vector3 screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);//Mouse Position
        Vector3 screenPosition = new Vector3(touch.position.x, touch.position.y);//Mouse Position
        Vector3 lookPos = Camera.main.ScreenToWorldPoint(screenPosition);//End point of a ray
        lookPos = lookPos - transform.position;
        float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
        //---Important variables---//

        transform.rotation = Quaternion.Euler(0, 0, angle);//Set rotation

        if (currentShootFrequency >= 0.1) currentShootFrequency -= 1f * Time.deltaTime;//Setting a frequency of shooting

        //---Reload section---//
        if (isShooting == false && currentAmmo <= ammo && isRechardged == true)//Recharging only If is able to shoot
        {
            currentAmmo += 40 * Time.deltaTime;//Reloading
            if(currentAmmo >= 98.5f && currentAmmo <= 99f) soundInstance.audioSource.PlayOneShot(soundInstance.rechargeClip, 1f);//Reload Sound
        }
        //---Reload section---//

        //---Global reload section---//
        if(isRechardged == false) currentAmmo += 70 * Time.deltaTime;//Recharging after full ammo empty        
        if (currentAmmo >= ammo) isRechardged = true;//Recharging
        if (currentAmmo <= 0) { currentAmmo = 0; }
        if (currentAmmo == 0) { isRechardged = false; soundInstance.audioSource.PlayOneShot(soundInstance.emptyAmmo, 0.5f); }
        //---Global reload section---//
       
    }

    public void Shoot(Rigidbody2D playerRb)
    {
        if (currentAmmo >= 0 && currentShootFrequency <= 0.2 && isRechardged == true)
        {            
            int shootClipIndex = Random.Range(0, soundInstance.shootClips.Length);//Choose a random sound of shooting
            soundInstance.audioSource.PlayOneShot(soundInstance.shootClips[shootClipIndex], 1f);//Shooting sound
            currentAmmo -= gun.ammoFrequency;//Minus an ammo
            currentShootFrequency = shootFrequency;//Reset the frequency value
            Instantiate(bullet, firePoint.position, Quaternion.identity);
            Instantiate(shootExplosion, firePoint.position, Quaternion.identity);
            Bullet.instance.rb.AddForce(firePoint.right * gun.bulletSpeed, ForceMode2D.Impulse);//Give a force for bullet
            playerRb.AddForce(-transform.right * recoilForce, ForceMode2D.Impulse);//Recoil for player
        }
    }

    public IEnumerator MiniGun()
    {
        float minigunLifeTime = 1.3f;
        float frequency = 1f;
        bool isRechardgedMiniGun = true;
        while (minigunLifeTime > 0.05)
        {
            frequency -= 1f * Time.deltaTime;
            if (frequency <= 5)
                isRechardgedMiniGun = true;
            minigunLifeTime -= 1f * Time.deltaTime;

            if (isRechardgedMiniGun)
            {
                GameManager.instance.player.simulated = false;
                isMinigunShoot = true;
                transform.Rotate(transform.forward * 5);

                currentShootFrequency = shootFrequency;//Reset the frequency value
                int shootClipIndex = Random.Range(0, soundInstance.shootClips.Length);//Choose a random sound of shooting
                soundInstance.audioSource.PlayOneShot(soundInstance.shootClips[shootClipIndex], 0.5f);//Shooting sound
                Instantiate(bullet, firePoint.position, Quaternion.identity);
                Instantiate(shootExplosion, firePoint.position, Quaternion.identity);
                Bullet.instance.rb.AddForce(firePoint.right * gun.bulletSpeed, ForceMode2D.Impulse);//Give a force for bullet
                isRechardgedMiniGun = false;
            }
            yield return null;
        }
        GameManager.instance.player.simulated = true;
        GameManager.instance.player.velocity = new Vector2(GameManager.instance.player.velocity.x, 0);
        isMinigunShoot = false;
    }

}
