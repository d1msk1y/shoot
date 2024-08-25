using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    public int gunIndex;
    //public GunSelectButton gunButton;
    public Gun selectedGun;
    public static UpgradeSystem instance;
    public GunSelectButton pistol, riffle, shotgun;
    public Sprite[] gunSprites;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        gunIndex = PlayerPrefs.GetInt("gunIndex");
    }

    // Update is called once per frame
    void Update()
    {
        
        gunIndex = PlayerPrefs.GetInt("gunIndex");
        if(gunIndex == 0)
        {
            selectedGun = pistol.currentGun;
        }
        if(gunIndex == 1)
        {
            selectedGun = riffle.currentGun;
        }
        if(gunIndex == 2)
        {
            selectedGun = shotgun.currentGun;
        }
    }



}
