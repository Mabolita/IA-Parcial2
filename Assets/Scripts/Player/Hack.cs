using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hack : MonoBehaviour
{
    public float _rangeHack;

    public Hack(float rangeHack)
    {
        _rangeHack = rangeHack;
    }

    public void ActiveHack(Transform player)
    {
        List<EnemyAI> enemies = new List<EnemyAI>();
        RaycastHit[] hits = Physics.SphereCastAll(player.position, _rangeHack, player.forward, 10);
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
}
