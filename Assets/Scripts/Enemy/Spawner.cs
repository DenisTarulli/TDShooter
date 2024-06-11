using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnCooldown;
    [SerializeField] private float startSpawningCooldown;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), startSpawningCooldown, spawnCooldown) ;
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}
