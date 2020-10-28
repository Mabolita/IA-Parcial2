using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxAnimator : MonoBehaviour
{
    public Animator anim;
    public Collider spColl;
    public ParticleSystem springPS;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        spColl = GetComponent<Collider>();
    }


    // Update is called once per frame
   

    private void OnTriggerEnter(Collider spColl)
    {
        if (spColl.gameObject.CompareTag("Player"))
        {
            anim.SetBool("OpenBox",true);
            print("Toco Player");
            GetComponent<BoxLoot>().RandomPower();
            //ParticleSystem obj = Instantiate(springPS, transform.position, transform.rotation);
            //obj.Play();
            //Destroy(obj.gameObject, 2f);
        }

    }
}
