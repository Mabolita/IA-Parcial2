using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidance : ISteering
{
    float _radius;
    float _avoidWeigth;
    Transform _from;
    Transform _target;
    LayerMask _mask;

    public ObstacleAvoidance(Transform from, float radius, LayerMask mask, float avoidWeigth, Transform target)
    {
        _from = from;
        _radius = radius;
        _mask = mask;
        _target = target;
        _avoidWeigth = avoidWeigth;
    }

    public Vector3 GetDir()
    {
        Vector3 dir = (_target.position - _from.position).normalized;

        var obstacles = Physics.OverlapSphere(_from.position, _radius, _mask);
        if (obstacles.Length > 0)
        {
            float distance = (obstacles[0].transform.position - _from.transform.position).magnitude;
            int indexSave = 0;
            for (int i = 1; i < obstacles.Length; i++)
            {
                float currentDist = (obstacles[i].transform.position - _from.transform.position).magnitude;
                if (currentDist < distance)
                {
                    distance = currentDist;
                    indexSave = i;
                }
            }
            Vector3 dirFromObs = (_from.position - obstacles[indexSave].transform.position).normalized * ((_radius - distance) / _radius) * _avoidWeigth;
            dir += dirFromObs;
        }
        return dir.normalized;
    }
}
