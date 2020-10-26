using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUps: MonoBehaviour
{
    public float _rotationSpeed;
    public Collider coll;
    public ParticleSystem PS;
    public float height;
    public float speed;
    int changeDir=1;
    Vector3 startPos;


    // Start is called before the first frame update
    void Start()
    {
        //startPos=new Vector3(0,transform.position.y,0);
  }

    void Update()
    {
        transform.Rotate(0, _rotationSpeed, 0);
        //transform.localRotation.eulerAngles+=new Vector3(0, _rotationSpeed,0);
        /*
        transform.position+=new Vector3(0,speed*changeDir*Time.deltaTime, 0);
        transform.Rotate(0, _rotationSpeed, 0);

        if(transform.position.y>=startPos.y*height)
        {
            changeDir=-1;
        }
        if (transform.position.y <= -startPos.y * height)
        {
            changeDir = 1;
        }*/
    }


    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            ParticleSystem obj = Instantiate(PS, transform.position, transform.rotation);
            obj.Play();
            Destroy(obj.gameObject, 3f);
            gameObject.SetActive(false);
            Destroy(gameObject,5f);
        }

    }

}
