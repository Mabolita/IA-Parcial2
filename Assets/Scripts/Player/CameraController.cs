using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController
{
    public Transform _camPivot;
    public Transform _pl;

    public float _xSensitivity = 100.0f;
    public float _ySensitivity = 100.0f;
    public float _yMinLimit = -45.0f;
    public float _yMaxLimit = 45.0f;
    public float _xMinLimit = -360.0f;
    public float _xMaxLimit = 360.0f;

    float _yRot = 0.0f;
    float _xRot = 0.0f;

    public CameraController(Transform camPivot, float xSensitivity, float ySensitivity,
                            float yMinLimit, float yMaxLimit, float xMinLimit,
                            float xMaxLimit, float yRot, float xRot, Transform player)
    {
        _camPivot = camPivot;
        _xSensitivity = xSensitivity;
        _ySensitivity = ySensitivity;
        _yMinLimit = yMinLimit;
        _yMaxLimit = yMaxLimit;
        _xMinLimit = xMinLimit;
        _xMaxLimit = xMaxLimit;
        _yRot = yRot;
        _xRot = xRot;
        _pl = player;
    }

    public void RotateCam()
    {
        _xRot += Input.GetAxis("Mouse X") * _xSensitivity * Time.deltaTime;
        _yRot += Input.GetAxis("Mouse Y") * _ySensitivity * Time.deltaTime;
        _yRot = Mathf.Clamp(_yRot, _yMinLimit, _yMaxLimit);
        _camPivot.localEulerAngles = new Vector3(-_yRot, _camPivot.localEulerAngles.y, 180);
        _pl.localEulerAngles = new Vector3(_pl.localEulerAngles.x, _xRot, _pl.localEulerAngles.z);
    }
}
