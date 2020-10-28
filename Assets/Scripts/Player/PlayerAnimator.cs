using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator anim;
    public Collider trigcoll;
    private Rigidbody rb;
    private PlayerController player;
    public float stepcooldown;
    float _stepcooldown=0.3f;
    bool step;
    void Start()
    {
        player = GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        anim.SetBool("Walk", true);
        stepcooldown = _stepcooldown;
    }

    void Update()
    {
        stepcooldown -= Time.deltaTime;
        if(stepcooldown<=0)
        {
            step = true;
        }
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            anim.SetBool("Walk", true);
            anim.SetFloat("SpeedZ", Input.GetAxis("Vertical"));
            anim.SetFloat("SpeedX", Input.GetAxis("Horizontal"));
            
            if (step == true)
            {
                player.audioSource.PlayOneShot(player.walkSound);
                step = false;
                stepcooldown = _stepcooldown;
            }

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("Walk", false);
            anim.SetTrigger("Jump");
            player.audioSource.PlayOneShot(player.jumpSound);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (player.Dash && !player.hack && player.powerTimer <= player.powerTimerMax)
            {
                anim.SetTrigger("Dash");
                player.audioSource.PlayOneShot(player.dashSound);
            }
            else if(!player.Dash && player.hack && player.powerTimer <= player.powerTimerMax)
            {
                anim.SetTrigger("Hack");
                player.audioSource.PlayOneShot(player.hackSound);
            }
        }
    }


    private void OnTriggerEnter(Collider _trigcoll)
    {
        if (_trigcoll.gameObject.CompareTag("Win"))
        {
            anim.SetBool("Idle",false);
            anim.SetBool("Win",true);
            
        }

        if (_trigcoll.gameObject.CompareTag("Bullet") || _trigcoll.gameObject.CompareTag("Death"))
        {
            anim.SetTrigger("Death");
            player.audioSource.PlayOneShot(player.deathSound);
        }
    }

}
