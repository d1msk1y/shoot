using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{

    public static CameraShake instance;
    [SerializeField]private CinemachineVirtualCamera VirCam;

    private void Awake()
    {
        instance = this;
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        var transposer = VirCam.GetCinemachineComponent<CinemachineTransposer>();
        Vector3 originalPos = transposer.m_FollowOffset;

        float elapsed = 0.0f;

        while(elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transposer.m_FollowOffset = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;

    }

}
