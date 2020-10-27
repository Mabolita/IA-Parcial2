using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hack : MonoBehaviour
{
    public float range;
    public Transform player;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ActiveHack();
        }
    }

    public void ActiveHack()
    {
        List<EnemyAI> enemies = new List<EnemyAI>();
        RaycastHit[] hits = Physics.SphereCastAll(player.position, range, player.forward, 10);
        if (hits.Length > 0)
        {
            foreach (var item in hits)
            {
                EnemyAI cItem = item.transform.GetComponent<EnemyAI>();
                if (cItem != null)
                    enemies.Add(cItem);
            }
            foreach (var enemy in enemies)
            {
                enemy.hack = true;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(player.position, range);
    }
}
