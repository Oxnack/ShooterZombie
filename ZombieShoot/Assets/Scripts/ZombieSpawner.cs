using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour                   // писал gpt
{
    [SerializeField] private GameObject _zombiePrefab;
    public Vector3 spawnZoneMin;
    public Vector3 spawnZoneMax;
    public float initialSpawnInterval = 1f;
    public float spawnIntervalDecreaseRate = 0.1f;
    public float minSpawnInterval = 0.1f;

    private float currentSpawnInterval;

    void Start()
    {
        currentSpawnInterval = initialSpawnInterval;
        InvokeRepeating("SpawnPrefab", 0f, currentSpawnInterval);
    }

    void SpawnPrefab()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(spawnZoneMin.x, spawnZoneMax.x), Random.Range(spawnZoneMin.y, spawnZoneMax.y), Random.Range(spawnZoneMin.z, spawnZoneMax.z));
        Instantiate(_zombiePrefab, spawnPosition, Quaternion.identity);

        // Уменьшаем интервал спавна
        currentSpawnInterval = Mathf.Max(currentSpawnInterval - spawnIntervalDecreaseRate, minSpawnInterval);
        CancelInvoke("SpawnPrefab");
        InvokeRepeating("SpawnPrefab", currentSpawnInterval, currentSpawnInterval);
    }

}
