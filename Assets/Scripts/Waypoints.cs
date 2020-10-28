using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Waypoints : MonoBehaviour
{
    public float speed;
    public Transform target;
    private Vector3 start, end;
    private bool _lookRight = false;
    public bool isCarPlatform;
    public PlatformSpawner platformSpawner;

    private void Start()
    {
        if (target != null)
        {
            target.parent = null;
            start = transform.position;
            end = target.position;
        }
    }

    private void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            if (transform.position == target.position)
            {
                Flip();
            }
        }
    }

    void Flip()
    {
        if (!isCarPlatform)
        {
            target.position = (target.position == start) ? end : start;
            _lookRight = !_lookRight;
            transform.Rotate(0, 180, 0);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
