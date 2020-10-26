using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 2;
    public Transform enemy;
    private void Update()
    {
        transform.up = Vector3.ProjectOnPlane(transform.up, Vector3.up);
        transform.position += transform.up * speed * Time.deltaTime;
        if (Vector3.Distance(enemy.position, transform.position) > 20000)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
