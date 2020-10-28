using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator anim;
    public Collider trigcoll;
    private Rigidbody rb;
    private PlayerController player;
    
    void Start()
    {
        player = GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        anim.SetBool("Walk", true);
    }

    void Update()
    {
        //CAMINATA
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            anim.SetBool("Walk", true);
            anim.SetFloat("SpeedZ", Input.GetAxis("Vertical"));
            anim.SetFloat("SpeedX", Input.GetAxis("Horizontal"));
        }

        //SALTO Y DASH

        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("Walk", false);
            anim.SetTrigger("Jump");
            print("Jump");
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //anim.SetBool("Walk", true);
            if (player.Dash && !player.hack)
            {
                anim.SetTrigger("Dash");
            }
            else
                anim.SetTrigger("Hack");
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
        }
    }

}
