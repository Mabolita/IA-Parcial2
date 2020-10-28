﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CameraController _cc;
    private CapsuleCollider capsuleCollider;
    private Dash _d;
    private Hack _h;
    private Animator _anim;
    private Vector3 normalHit;

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


    [Header("Componentes")]
    public Rigidbody rigidBody;
    public List<string> tags = new List<string>();
    public LayerMask layerMask;
    public Transform camPivot;

    [Header("Sonidos")]
    public AudioSource audioSource;
    public AudioClip jumpSound, hackSound, deathSound, dashSound,walkSound;

    [Header("Variables")]
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
    public float rangeHack;
    public float powerTimerMax;
    public float powerTimer;
    public bool Dash;
    public bool hack;

    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        rigidBody = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _d = new Dash(layerMask, distance, transform, camPivot);
        _h = new Hack(rangeHack);
        _cc = new CameraController(camPivot, xSensitivity, ySensitivity, yMinLimit, yMaxLimit, xMinLimit, xMaxLimit, yRot, xRot, transform);


        xSensitivity = Sensitivity;
        ySensitivity = Sensitivity;
        yMinLimit = -yLimit;
        yMaxLimit = yLimit;
        xMinLimit = -xLimit;
        xMaxLimit = xLimit;
        powerTimer = powerTimerMax;

        //_anim.SetBool("Idle", true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Vector3 check = GameManager.Instance.CurrentCheckPoint.position;
            transform.position = check;
        }

        if (powerTimer < powerTimerMax)
        {
            powerTimer += Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && powerTimer >= powerTimerMax)
        {
            if (Dash)
            {
                _d.DashC();
            }
            if (hack)
            {
                _h.ActiveHack(transform);
            }
            powerTimer = 0;
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
                rigidBody.velocity = new Vector3(0, rigidBody.velocity.y, 0);
            }
            rigidBody.freezeRotation = rigidBody;
        }

    }

    public void Jump()
    {
        if (jumps < maxCantJumps)
        {
            jumps++;
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0, rigidBody.velocity.z);
            Vector3 force = transform.up * jumpForce;
            rigidBody.AddForce(force, ForceMode.Force);
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
        rigidBody.velocity = new Vector3(move.x, rigidBody.velocity.y, move.z);
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
            rigidBody.velocity = Vector3.zero;
            rigidBody.freezeRotation = rigidBody;
        }
        else
        {
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0, rigidBody.velocity.z);
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
            rigidBody.velocity += new Vector3(normalHit.x * slideVelocity, -slideForceDown, normalHit.z * slideVelocity) * Time.deltaTime;
        }
    }
}
