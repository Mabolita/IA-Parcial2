using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator anim;
    public Collider trigcoll;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("Idle",true);
    }

    // Update is called once per frame
    void Update()
    {
        //CAMINATA
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
           Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            anim.SetBool("Idle", false);
            anim.SetBool("Walk", true);
            print("Walk");
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) ||
           Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            anim.SetBool("Idle", true);
            anim.SetBool("Walk", false);
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
