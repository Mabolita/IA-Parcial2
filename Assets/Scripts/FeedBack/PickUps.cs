﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUps : MonoBehaviour
{
    public float _rotationSpeed;
    public float height;
    public float speed;
    public Collider coll;
    public ParticleSystem PS;
    int changeDir = 1;
    Vector3 startPos;
    public bool powerDash;
    public bool powerHack;
    AudioSource _audioSource;
    public AudioClip _audioClip;

    private CanvasController _cc;

    private void Awake()
    {
        _cc = FindObjectOfType<CanvasController>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        transform.Rotate(0, _rotationSpeed, 0);
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            ParticleSystem obj = Instantiate(PS, transform.position, transform.rotation);
            obj.Play();
            Destroy(obj.gameObject, 1f);
            _audioSource.PlayOneShot(_audioClip);
            gameObject.GetComponent<MeshRenderer>().enabled=false;
            Destroy(gameObject, _audioClip.length);
            _cc.gearsCount++;
        }
    }
        
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (powerDash)
            {
                collision.gameObject.GetComponent<PlayerController>().Dash = true;
                FindObjectOfType<CanvasController>().dash = true;
                Destroy(gameObject);
            }
            else if (powerHack)
            {
                collision.gameObject.GetComponent<PlayerController>().hack = true;
                FindObjectOfType<CanvasController>().hack = true;
                Destroy(gameObject);
            }
        }
    }

}
