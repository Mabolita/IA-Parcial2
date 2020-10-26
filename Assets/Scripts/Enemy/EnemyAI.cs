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
    public LayerMask _lm;

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
        sm = new StateMachine();
        sm.AddState(new EnemyPatrolState(sm, this));
        sm.AddState(new EnemyIdleState(sm, this));
        sm.AddState(new EnemyShootState(sm, this));
        sm.AddState(new EnemySeekState(sm, this));
        visionRange = GetComponent<SphereCollider>();
        animator = GetComponent<Animator>();
        enemyTree = new EnemyDecisionTree(this);
        enemyTree.SetNodes();
        //sm.SetState<EnemyPatrolState>();
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
            LineOfSight();
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
        Bullet bullet = Object.Instantiate(bulletPrefab,bulletSpawn.position,bulletSpawn.rotation).GetComponent<Bullet>();
        bullet.transform.up = bulletSpawn.forward;
        bullet.enemy = transform;
    }

    bool LineOfSight()
    {
        Vector3 dirToPlayer = (player.transform.position - transform.position).normalized;
        if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle)
        {
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(transform.position, dirToPlayer,out hit, visionRange.radius, _lm))
            {
                playerInSight = hit.transform.gameObject.layer == 8;
            }
            else
            {
                return false;
            }
            
        }
        return playerInSight;
    }

    public void ActionShoot()
    {
        Debug.Log("shoot");
        sm.SetState<EnemyShootState>();
    }

    public void ActionSeek()
    {
        Debug.Log("seek");
        sm.SetState<EnemySeekState>();
    }
    public void ActionPatrol()
    {
        Debug.Log("patrol");
        sm.SetState<EnemyPatrolState>();
    }

    public bool QuestionDistanceShoot()
    {
        Debug.Log("Distaceshoot");
        return Vector3.Distance(transform.position, player.transform.position) < distanceToShoot;
    }

    public bool QuestionIsPlayerOnSight()
    {
        Debug.Log("sight");
        return LineOfSight();
    }
}
