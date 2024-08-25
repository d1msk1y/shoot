using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Guns/New Gun")]
public class Gun : ScriptableObject
{
    new public string name = "New Gun";
    public float shootFrequency = 1f;
    [HideInInspector]public float ammo = 100f;
    public float ammoFrequency = 10f;
    public float bulletSpeed = 10f;
    public float recoilForce = 30f;
    public int upgradeCost;
}
