using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSpawn : MonoBehaviour
{
    public GameObject spawnPoint;
    private Vector3 max;
    private Vector3 min;
    public Vector3 spawnLength;
    public Vector3 offset;
    private Vector3 whereToSpawn;
    public GameObject toInstatniate;
    public GameObject[] enemies;
    public int maxEnemies;
    private int currentEnemies;
    private float minX, minY;
    public bool destroy = false;

    private void Awake()
    {
        SetSize();
    }

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
        if (destroy == true)
        {
            StartCoroutine(StartDestroy());
        }

    }

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        currentEnemies = enemies.Length;

        Spawn();

        SetSize();
    }

    IEnumerator StartDestroy()
    {
        yield return new WaitForSeconds(1f);
        this.enabled = false;
    }

    void SetSize()
    {
        min = spawnPoint.transform.position + offset;
        max = min + gameObject.transform.lossyScale;

        minX = Random.Range(min.x, max.x);
        minY = Random.Range(min.y, max.y);

        whereToSpawn = new Vector3(minX, minY);
    }

    void Spawn()
    {
        if (currentEnemies < maxEnemies)
        {
            Instantiate(toInstatniate, whereToSpawn, Quaternion.identity);
        }
    }
}
