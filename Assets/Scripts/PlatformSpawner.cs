using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public List<GameObject> platforms = new List<GameObject>();
    public float maxSpawnRate;
    public float distanceSpawn;
    public float timeToSpawn;
    float time;
    public Transform platformTarget;

    private void Update()
    {
        time-=Time.deltaTime;
        if (time < 0)
        {
            Spawn();
            time = timeToSpawn;
        }
    }

    public void Spawn()
    {
        var randomPlatform = Random.Range(0, platforms.Count - 1);
        Waypoints waypoints = Instantiate(platforms[randomPlatform], transform.position, Quaternion.identity).GetComponent<Waypoints>();
        waypoints.target = platformTarget;
    }
}
