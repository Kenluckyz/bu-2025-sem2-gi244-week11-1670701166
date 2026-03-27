using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerUpPrefab;

    public Transform[] spawnPoints;

    public Wave[] waves;

    void Start()
    {
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        for (int i = 0; i < waves.Length; i++)
        {
            Wave w = waves[i];

            Debug.Log("Wave " + (i + 1));

            // spawn powerup ก่อน
            for (int j = 0; j < w.numberOfPowerUp; j++)
            {
                Transform p = spawnPoints[Random.Range(0, spawnPoints.Length)];
                Instantiate(powerUpPrefab, p.position, Quaternion.identity);
              
            }

            // รอก่อนเริ่ม spawn
            yield return new WaitForSeconds(w.delayStart);

            // สุ่ม spawn point
            List<Transform> usePoints = new List<Transform>();

            while (usePoints.Count < w.numberOfRandomSpawnPoint)
            {
                Transform p = spawnPoints[Random.Range(0, spawnPoints.Length)];

                if (!usePoints.Contains(p))
                {
                    usePoints.Add(p);
                }
            }

            // spawn enemy ทีละตัว
            for (int j = 0; j < w.totalSpawnEnemies; j++)
            {
                Transform spawn = usePoints[Random.Range(0, usePoints.Count)];

                Instantiate(enemyPrefab, spawn.position, Quaternion.identity);

                yield return new WaitForSeconds(w.spawnInterval);
            }

            // รอให้ enemy หมดก่อน wave ต่อไป
            yield return new WaitUntil(() =>
                GameObject.FindGameObjectsWithTag("Enemy").Length == 0
            );
        }

        Debug.Log("Finish All Waves");
    }
}