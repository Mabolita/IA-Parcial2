using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash
{
    public LayerMask _lm;
    public Transform _pl;
    public Transform _camPivot;

    public float _distance;

    public Dash(LayerMask lm, float distance, Transform player,Transform camPivot)
    {
        _lm = lm;
        _distance = distance;
        _pl = player;
        _camPivot = camPivot;
    }

    public void DashC()
    {
        RaycastHit hit = new RaycastHit();
        Vector3 initialPos = _pl.position;
        if (Physics.Raycast(_pl.position, _pl.forward, out hit, _distance, _lm))
        {
           _pl.position = hit.point - (_pl.forward / 2);
        }
        else
        {
            _pl.position += _pl.forward * _distance;
        }
        _camPivot.position = _pl.position;
    }

}
