using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float minIdleTime = 3;
    public float maxIdleTime = 5;
    public float fireRate = 1.5f;
    public float viewAngle = 45;
    public float distanceToShoot;

    public Transform bulletSpawn;
    public GameObject bulletPrefab;

    public PlayerController player;

    public List<Transform> waypoints = new List<Transform>();
    public int currentWaypointTarget = 0;

    public bool playerInRange = false;
    public bool playerInSight = false;

    public Animator animator;
    public SphereCollider visionRange;

    public StateMachine sm;
    public EnemyDecisionTree enemyTree;


    void Awake()
    {
        enemyTree = new EnemyDecisionTree();
        visionRange = GetComponent<SphereCollider>();
        animator = GetComponent<Animator>();
        sm = new StateMachine();
        sm.AddState(new EnemyPatrolState(sm, this));
        sm.AddState(new EnemyIdleState(sm, this));
        sm.AddState(new EnemyShootState(sm, this));
        sm.SetState<EnemyPatrolState>();
    }

    private void Start()
    {
        enemyTree._init.Execute();
    }

    void Update()
    {
        sm.Update();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            playerInRange = true;
            player = other.GetComponent<PlayerController>();
            //LineOfSight();
            enemyTree._init.Execute();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            playerInRange = false;
            playerInSight = false;
            enemyTree._init.Execute();
        }
    }
    public void OnAnimatorShoot()
    {
        GameObject bullet = Object.Instantiate(bulletPrefab);
        bullet.transform.position = bulletSpawn.position;
        bullet.transform.up = bulletSpawn.forward;
    }

    bool LineOfSight()
    {
        Vector3 dirToPlayer = (player.transform.position - transform.position).normalized;
        if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle)
        {
            playerInSight = !Physics.Raycast(transform.position, dirToPlayer, visionRange.radius, 1 << 8);
        }
        return playerInSight;
    }

    public void ActionShoot()
    {
        sm.SetState<EnemyShootState>();
    }

    public void ActionSeek()
    {
        sm.SetState<EnemySeekState>();
    }
    public void ActionPatrol()
    {
        sm.SetState<EnemyPatrolState>();
    }

    public bool QuestionDistanceShoot()
    {
        return Vector3.Distance(transform.position, player.transform.position) < distanceToShoot;
    }

    public bool QuestionIsPlayerOnSight()
    {
        return LineOfSight();
    }
}
