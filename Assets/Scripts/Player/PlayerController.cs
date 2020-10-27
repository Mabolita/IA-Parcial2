using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CameraController _cc;
    private CapsuleCollider capsuleCollider;
    private Dash _d;
    public Rigidbody _rb;
    private Animator _anim;
    private Vector3 normalHit;

    public List<string> tags = new List<string>();

    private float xSensitivity = 100.0f;
    private float ySensitivity = 100.0f;
    private float yMinLimit = -45.0f;
    private float yMaxLimit = 45.0f;
    private float xMinLimit = -360.0f;
    private float xMaxLimit = 360.0f;
    private float yRot = 0.0f;
    private float xRot = 0.0f;
    private int jumps;
    private bool isOnSlope;

    public LayerMask lm;
    public Transform camPivot;

    public int maxCantJumps;
    public float jumpForce;
    public float slopeLimit;
    public float slideVelocity;
    public float slideForceDown;
    public float moveSpeed;
    public float MaxMoveSpeed;
    public float distance;
    public float Sensitivity = 100.0f;
    public float yLimit = 45.0f;
    public float xLimit = 360.0f;



    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
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
            Vector3 check = GameManager.Instance.CurrentCheckPoint.position;
            transform.position = check;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _d.DashC();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetAxis("Mouse Y") != 0 || Input.GetAxis("Mouse X") != 0)
        {
            _cc.RotateCam();
        }

        FixMove();

    }

    public void FixMove()
    {

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            Move();
        }
        else
        {
            if (!isOnSlope)
            {
                _rb.velocity = new Vector3(0, _rb.velocity.y, 0);
            }
            _rb.freezeRotation = _rb;
        }

    }

    public void Jump()
    {
        if (jumps < maxCantJumps)
        {
            jumps++;
            _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
            Vector3 force = transform.up * jumpForce;
            _rb.AddForce(force, ForceMode.Force);
        }
    }

    public void Move()
    {
        float inputX = 0;
        float inputY = 0;
        inputX = SetInputX(inputX);
        inputY = SetInputY(inputY);
        Vector3 inputs = Vector3.ClampMagnitude(new Vector3(inputX, 0, inputY), 1);
        Vector3 dir = inputs.x * transform.right + inputs.z * transform.forward;

        Vector3 move = (dir * moveSpeed * Time.deltaTime);
        move = Vector3.ClampMagnitude(move, MaxMoveSpeed);
        _rb.velocity = new Vector3(move.x, _rb.velocity.y, move.z);
    }

    public float SetInputX(float x)
    {
        if (Input.GetKey(KeyCode.D))
        {
            x = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            x = -1;
        }
        else
        {
            x = 0;
        }
        return x;
    }

    public float SetInputY(float y)
    {
        if (Input.GetKey(KeyCode.W))
        {
            y = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            y = -1;
        }
        else
        {
            y = 0;
        }
        return y;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != 9)
        {
            _rb.velocity = Vector3.zero;
            _rb.freezeRotation = _rb;
        }
        else
        {
            _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (var tag in tags)
        {
            if (other.gameObject.CompareTag(tag))
            {
                capsuleCollider.center = new Vector3(0, 1.5f, 0);
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            if (jumps != 0 && !isOnSlope)
            {
                jumps = 0;
            }
            normalHit = collision.GetContact(0).normal;
            SlideDown();
        }
    }

    public void SlideDown()
    {
        isOnSlope = Vector3.Angle(Vector3.up, normalHit) >= slopeLimit;
        if (isOnSlope)
        {
            _rb.velocity += new Vector3(normalHit.x * slideVelocity, -slideForceDown, normalHit.z * slideVelocity) * Time.deltaTime;
        }
    }
}
