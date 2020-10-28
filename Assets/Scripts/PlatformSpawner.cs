using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public List<GameObject> platforms = new List<GameObject>();
    public float maxSpawnRate;
    public float distanceSpawn;
    public float timeToSpawn;
    public Transform platformTarget;

    private void Update()
    {
        timeToSpawn++;
        if (timeToSpawn > maxSpawnRate)
        {
            Spawn();
            timeToSpawn = Random.Range(0, maxSpawnRate - distanceSpawn);
        }
    }

    public void Spawn()
    {
        var randomPlatform = Random.Range(0, platforms.Count - 1);
        Waypoints waypoints = Instantiate(platforms[randomPlatform], transform.position, Quaternion.identity).GetComponent<Waypoints>();
        waypoints.target = platformTarget;
    }
}
