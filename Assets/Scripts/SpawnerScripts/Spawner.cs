using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameManagerScript gameSpeedController;
    [Header("Obstacle Settings")]
    [SerializeField] GameObject lightObstaclePrefab;
    [SerializeField] GameObject heavyObstaclePrefab;
    [Header("Bonus Settings")]
    [SerializeField] GameObject healBonusPrefab;
    [SerializeField] GameObject invulnerabilityBonusPrefab;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnDistance = 30f;
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private float[] spawnLinesX = {-3.33f, 0f, 3.33f};
    [SerializeField] private float despawnDistance = 10f;

    [Header("Player Settings")]
    [SerializeField] private Transform _playerTransform;

    private readonly List<GameObject> _spawnedObstacles = new List<GameObject>();
    private readonly List<GameObject> _spawnedBonuses = new List<GameObject>();

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
            SpawnObjects();
        }
        DestroyPassedObjects();
    }

    private void SpawnObjects()
    {
        bool[] occupiedLanes = new bool[spawnLinesX.Length];
        int numberLight = Random.Range(0, spawnLinesX.Length);
        float laneLight = spawnLinesX[numberLight];
        _spawnedObstacles.Add(
            Instantiate(lightObstaclePrefab,
            new Vector3(laneLight, 2, _playerTransform.position.z + spawnDistance),
            Quaternion.identity)
            );
        occupiedLanes[numberLight] = true; 
        int numberHeavy = Random.Range(0, spawnLinesX.Length);
        float laneHeavy = spawnLinesX[numberHeavy];
        if (!occupiedLanes[numberHeavy] && Random.value < 0.5f)
        {
            _spawnedObstacles.Add(
                Instantiate(heavyObstaclePrefab,
                new Vector3(laneHeavy, 2, _playerTransform.position.z + spawnDistance),
                Quaternion.identity)
                );
            occupiedLanes[numberHeavy] = true;
        }
        int numberInvuln = Random.Range(0, spawnLinesX.Length);
        float laneInvuln = spawnLinesX[numberInvuln];
        if (!occupiedLanes[numberInvuln] && Random.value < 0.3f)
        {
            _spawnedBonuses.Add(
                Instantiate(healBonusPrefab, 
                    new Vector3(laneInvuln, 2, _playerTransform.position.z + spawnDistance),
                    Quaternion.identity)
            );
            occupiedLanes[numberInvuln] = true;
        }
        int numberHeal = Random.Range(0, spawnLinesX.Length);
        float laneHeal = spawnLinesX[numberHeal];
        if (!occupiedLanes[numberHeal] && Random.value < 0.3f)
        {
            _spawnedBonuses.Add(
                Instantiate(invulnerabilityBonusPrefab, 
                    new Vector3(laneHeal, 2, _playerTransform.position.z + spawnDistance),
                    Quaternion.identity)
            );
            occupiedLanes[numberHeal] = true;
        }
    }

    private void DestroyPassedObjects()
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
        for (int i = _spawnedBonuses.Count - 1; i >= 0; i--)
        {
            GameObject bonus = _spawnedBonuses[i]; 
            if (bonus && _playerTransform.position.z - bonus.transform.position.z > despawnDistance)
            {
                _spawnedBonuses.Remove(bonus);
                Destroy(bonus);
            }
        }
    }
}
