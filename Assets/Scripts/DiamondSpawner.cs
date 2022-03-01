using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondSpawner : MonoBehaviour
{
    public GameObject[] enemies;
    public Vector2 spawnValues;
    public float spawnWait;
    public float spawnMostWait;
    public float spawnLeastWait;
    public int startWait;
    public bool stop;

    int randEnemy;

    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(waitSpawner());

    }

    // Update is called once per frame
    void Update()
    {
        spawnWait = UnityEngine.Random.Range(spawnLeastWait, spawnMostWait);
    }

    IEnumerator waitSpawner()
    {
        yield return new WaitForSeconds(startWait);
        while (!stop)
        {
            randEnemy = Random.Range(0, enemies.Length);
            Vector2 spawnPosition = new Vector2(UnityEngine.Random.Range(-24f, -6f), transform.position.y);
            Instantiate(enemies[randEnemy], spawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(spawnWait);
        }
    }
}
