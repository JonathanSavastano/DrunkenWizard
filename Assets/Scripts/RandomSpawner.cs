using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private float spawnRadius = 5f; // Radius around the player

    private GameObject newEnemy;
    private SpriteRenderer rend;
    private float randomAngle;
    private Vector3 spawnPosition;

    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = FindObjectOfType<PlayerController>().transform;
        InvokeRepeating("SpawnNewEnemy", 0f, 3f);
    }

    private void SpawnNewEnemy()
    {
        // Generate a random angle in radians
        randomAngle = Random.Range(0f, Mathf.PI * 2f);

        // Calculate X and Y positions using polar coordinates
        float randomXposition = playerTransform.position.x + spawnRadius * Mathf.Cos(randomAngle);
        float randomYposition = playerTransform.position.y + spawnRadius * Mathf.Sin(randomAngle);

        spawnPosition = new Vector3(randomXposition, randomYposition, 0f);
        newEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity);
        rend = newEnemy.GetComponent<SpriteRenderer>();
    }
}
