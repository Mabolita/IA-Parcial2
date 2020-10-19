using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float minIdleTime = 3;
    public float maxIdleTime = 5;
    public float fireRate = 1.5f;
    public float viewAngle = 45;

    //public Transform bulletSpawn;
    //public GameObject bulletPrefab;

    public PlayerController player;

    public List<Transform> waypoints = new List<Transform>();
    public int currentWaypointTarget = 0;

    public bool playerInRange = false;
    public bool playerInSight = false;

    //public Animator animator;
    public SphereCollider sphereCollider;

    StateMachine sm;

    public float speed = 5;


    // Start is called before the first frame update
    void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
        //animator = GetComponent<Animator>();
        sm = new StateMachine();
        sm.AddState(new EnemyPatrolState(sm, this));
        //sm.AddState(new EnemyIdleState(sm, this));
        //sm.AddState(new EnemyShootingState(sm, this));
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
                playerInSight = !Physics.Raycast(transform.position, dirToPlayer, sphereCollider.radius, 1 << 8);
            }
        }
    }

    //public void OnAnimatorShoot()
    //{
    //    GameObject bullet = Object.Instantiate(bulletPrefab);
    //    bullet.transform.position = bulletSpawn.position;
    //    bullet.transform.up = bulletSpawn.forward;
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            player = other.GetComponent<PlayerController>();
            //entró el player en vision
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            //salió el player de vision
            playerInRange = false;
            playerInSight = false;
        }
    }

}
