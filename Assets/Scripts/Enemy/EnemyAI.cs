using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float minIdleTime = 3;
    public float maxIdleTime = 5;
    public float fireRate = 1.5f;
    public float viewAngle = 45;

    public Transform bulletSpawn;
    public GameObject bulletPrefab;

    public PlayerController player;

    public List<Transform> waypoints = new List<Transform>();
    public int currentWaypointTarget = 0;

    public bool playerInRange = false;
    public bool playerInSight = false;

    //public Animator animator;
    public SphereCollider visionRange;

    StateMachine sm;

    public float speed = 5;

    void Awake()
    {
        visionRange = GetComponent<SphereCollider>();
        //animator = GetComponent<Animator>();
        sm = new StateMachine();
        sm.AddState(new EnemyPatrolState(sm, this));
        sm.AddState(new EnemyIdleState(sm, this));
        sm.AddState(new EnemyShootState(sm, this));
        sm.SetState<EnemyPatrolState>();
    }

    void Update()
    {
        sm.Update();
        if (playerInRange)
        {
            Vector3 dirToPlayer = (player.transform.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle)
            {
                playerInSight = !Physics.Raycast(transform.position, dirToPlayer, visionRange.radius, 1 << 8);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            player = other.GetComponent<PlayerController>();
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            playerInRange = false;
            playerInSight = false;
        }
    }

}
