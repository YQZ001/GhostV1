using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

    public GameObject enemyPrefab;
    public float spawnTime = 10;
    private float timer = 0;

    void Start()
    {
        InvokeRepeating("ACC", 0, 1);
        SpawnEnemy();
    }
    
    void ACC()
    {
        spawnTime -= 0.02f;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= spawnTime)
        {
            timer -= spawnTime;
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        GameObject.Instantiate(enemyPrefab, transform.position, transform.rotation);
    }
}
