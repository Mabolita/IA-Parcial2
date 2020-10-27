using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator anim;
    public Collider trigcoll;
    private Rigidbody rb;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        anim.SetBool("Idle", true);
    }

    // Update is called once per frame
    void Update()
    {

        anim.SetFloat("SpeedX", rb.velocity.x);
        anim.SetFloat("SpeedZ", rb.velocity.z);


        //CAMINATA
        if (Input.GetAxis("Horizontal")!=0)
        {
            anim.SetBool("Walk", true);
            anim.SetBool("Idle", false);
            print(rb.velocity.x);
        }
        
        if(Input.GetAxis("Vertical") != 0)
        {
            anim.SetBool("Walk", true);
            anim.SetBool("Idle", false);
            //anim.SetFloat("SpeedZ", rb.velocity.z);
            print(rb.velocity.z);
        }

        if (Input.GetAxis("Horizontal") == 0 )
        {
            anim.SetBool("Walk", false);
            //anim.SetFloat("SpeedX", 0);
            anim.SetBool("Idle", true);
            //anim.SetBool("Walk", false);
            print("NotWalk");
        }

        if( Input.GetAxis("Vertical") == 0)
        {
            anim.SetBool("Walk", false);
            //anim.SetFloat("SpeedZ", 0);
            anim.SetBool("Idle", true);
            //anim.SetBool("Walk", false);
            print("NotWalk");
        }

        //SALTO Y DASH

        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("Idle", false);
            anim.SetTrigger("Jump");
            print("Jump");
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            anim.SetBool("Idle", true);
            anim.SetTrigger("Dash");
            print("Dash");
        }

    }

    

    private void OnTriggerEnter(Collider _trigcoll)
    {
        if (_trigcoll.gameObject.CompareTag("Win"))
        {
            anim.SetBool("Idle",false);
            anim.SetBool("Win",true);
        }

    }

}
