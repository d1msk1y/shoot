using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioLowPassFilter LowPasFilter;
    public AudioClip playerDieClip,rechargeClip, bulletHit,emptyAmmo,groundHit,coinHit;
    public AudioClip[] shootClips;
    public static SoundManager instance;
    public float lowPasCutOffSpeed,lowPasCutOffModifier;

    private void Awake()
    {
        instance = this;
    }
}
