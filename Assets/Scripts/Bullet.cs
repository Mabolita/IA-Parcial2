using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float maxSpeed;
    public Transform enemy;
    private void Update()
    {
        transform.up = Vector3.ProjectOnPlane(transform.up, Vector3.up);
        transform.position += transform.up * Random.Range(speed,maxSpeed) * Time.deltaTime;
        if (Vector3.Distance(enemy.position, transform.position) > 20000)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
