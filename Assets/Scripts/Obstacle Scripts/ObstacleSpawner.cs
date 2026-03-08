using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameSpeedController gameSpeedController;
    [Header("Obstacle Settings")]
    [SerializeField] GameObject lightObstaclePrefab;
    [SerializeField] GameObject heavyObstaclePrefab;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnDistance = 30f;
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private float[] spawnLinesX = {-3.33f, 0f, 3.33f};
    [SerializeField] private float despawnDistance = 10f;

    [Header("Player Settings")]
    [SerializeField] private Transform _playerTransform;

    private readonly List<GameObject> _spawnedObstacles = new List<GameObject>();

    private float timer;

    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += gameSpeedController.CurrentGameSpeed * Time.deltaTime;
        if (!_playerTransform || !lightObstaclePrefab || !heavyObstaclePrefab)
        {
            return;
        }
        if (timer >= spawnInterval)
        {
            timer = 0;
            SpawnObstacles();
        }
        DestroyPassedObstacles();
    }

    private void SpawnObstacles()
    {
        float laneLight = spawnLinesX[Random.Range(0, spawnLinesX.Length)];
        _spawnedObstacles.Add(
            Instantiate(lightObstaclePrefab,
            new Vector3(laneLight, 2, _playerTransform.position.z + spawnDistance),
            Quaternion.identity)
            );
        float laneHeavy = spawnLinesX[Random.Range(0, spawnLinesX.Length)];
        if (laneHeavy != laneLight && Random.value < 0.5f)
        {
            _spawnedObstacles.Add(
                Instantiate(heavyObstaclePrefab,
                new Vector3(laneHeavy, 2, _playerTransform.position.z + spawnDistance),
                Quaternion.identity)
                );
        }
    }

    private void DestroyPassedObstacles()
    {
        for (int i = _spawnedObstacles.Count - 1; i >= 0; i--)
        {
            GameObject obstacle = _spawnedObstacles[i]; 
            if (obstacle && _playerTransform.position.z - obstacle.transform.position.z > despawnDistance)
            {
                _spawnedObstacles.Remove(obstacle);
                Destroy(obstacle);
            }
        }
    }
}
