using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieWaveSystem : MonoBehaviour
{
    public GameObject[] zombiesPrefabs;
    public Transform[] spawnPoints;

    public float timeBetweenWaves = 10f;
    [SerializeField] private float waveTimer = 0f;

    private int waveNumber = 1;
    public int zombiesPerWave = 5;

    void Update()
    {
        if (waveNumber == 10)
            return;

        waveTimer += Time.deltaTime;
        int intValue = Mathf.RoundToInt(waveTimer);

        if(waveTimer >= timeBetweenWaves)
        {
            StartNewWave();
        }
    }
    void StartNewWave()
    {
        waveTimer = 0f;
        zombiesPerWave += 2;

        float minDistance = 4f;

        for (int i = 0; i < zombiesPerWave; i++)
        {
            int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomSpawnIndex];

            GameObject randomZombiePrefab = zombiesPrefabs[Random.Range(0, zombiesPrefabs.Length)];

            Vector3 spawnPosition = spawnPoint.position + Random.insideUnitSphere * minDistance;

            spawnPosition.y = spawnPoint.position.y;

            Instantiate(randomZombiePrefab, spawnPosition, spawnPoint.rotation);
        }

        waveNumber++;
    }
}

