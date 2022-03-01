using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemies;
    public Vector2 spawnValues;
    public float spawnWait;
    public float spawnMostWait;
    public float spawnLeastWait;
    public int startWait;
    public bool stop;

    int randEnemy;

    private GameObject player;
    private playerController playerScript;
    private int nbDiamonds;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<playerController>();
        
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
        while(!stop)
        {
            randEnemy = Random.Range(0, enemies.Length);
            Vector2 spawnPosition = new Vector2(UnityEngine.Random.Range(-24f, 9f), transform.position.y);
            nbDiamonds = playerScript.nbDiamonds;
            
            if(nbDiamonds == 0)
            {
                Instantiate(enemies[randEnemy], spawnPosition, Quaternion.identity).GetComponent<Rigidbody2D>().gravityScale = 2.0f;
            } else if (nbDiamonds == 1)
            {
                Instantiate(enemies[randEnemy], spawnPosition, Quaternion.identity).GetComponent<Rigidbody2D>().gravityScale = 10.0f;
            } else if (nbDiamonds == 2)
            {
                Instantiate(enemies[randEnemy], spawnPosition, Quaternion.identity).GetComponent<Rigidbody2D>().gravityScale = 15.0f;
            } else
            {
                stop = true;
            }
           
           

            yield return new WaitForSeconds(spawnWait);
        }
    }

}
