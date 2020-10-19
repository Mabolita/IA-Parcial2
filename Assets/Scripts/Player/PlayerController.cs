using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CameraController _cc;
    private Dash _d;
    private Rigidbody _rb;
    private Animator _anim;

    private float xSensitivity = 100.0f;
    private float ySensitivity = 100.0f;
    private float yMinLimit = -45.0f;
    private float yMaxLimit = 45.0f;
    private float xMinLimit = -360.0f;
    private float xMaxLimit = 360.0f;
    private float yRot = 0.0f;
    private float xRot = 0.0f;

    public LayerMask lm;
    public Transform camPivot;

    public float moveSpeed;
    public float MaxMoveSpeed;
    public float distance;
    public float Sensitivity = 100.0f;
    public float yLimit = 45.0f;
    public float xLimit = 360.0f;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _d = new Dash(lm, distance, transform, camPivot);
        _cc = new CameraController(camPivot, xSensitivity, ySensitivity, yMinLimit, yMaxLimit, xMinLimit, xMaxLimit, yRot, xRot, transform);
        

        xSensitivity = Sensitivity;
        ySensitivity = Sensitivity;
        yMinLimit = -yLimit;
        yMaxLimit = yLimit;
        xMinLimit = -xLimit;
        xMaxLimit = xLimit;

        //_anim.SetBool("Idle", true);
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            _d.DashC();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _d.DashC();
        }

        if (Input.GetAxis("Mouse Y") != 0 || Input.GetAxis("Mouse X") != 0)
        {
            _cc.RotateCam();
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            Vector3 move = (transform.right * Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime) + _rb.velocity;
            Vector3.ClampMagnitude(move, MaxMoveSpeed);
            _rb.velocity = Vector3.ClampMagnitude(move, MaxMoveSpeed);
           
        }

        if (Input.GetAxis("Vertical") != 0)
        {
            Vector3 move = (transform.forward * Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime) + _rb.velocity;
            Vector3.ClampMagnitude(move, MaxMoveSpeed);
            _rb.velocity = Vector3.ClampMagnitude(move, MaxMoveSpeed);
        }

        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            _rb.velocity = Vector3.zero;
            _rb.freezeRotation = _rb;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        _rb.velocity = Vector3.zero;
        _rb.freezeRotation = _rb;
    }
}
