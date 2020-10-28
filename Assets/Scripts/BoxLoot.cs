using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxLoot : MonoBehaviour
{
    public Transform spawn;
    public GameObject dash;
    public GameObject hack;
    public float forceThrow;
    public AudioSource audioSource;
    public AudioClip  open;

    private bool drop;
    private RoulettePower _rp;
    private Dictionary<GameObject, int> powers = new Dictionary<GameObject, int>();

    private void Awake()
    {
        _rp = new RoulettePower();
    }

    private void Start()
    {
        powers.Add(dash, 50);
        powers.Add(hack, 50);
    }

    public void RandomPower()
    {
        if (!drop)
        {
            audioSource.PlayOneShot(open);
            GameObject currentPower = _rp.Run(powers);
            GameObject p = Instantiate(currentPower, spawn.position, spawn.rotation);
            Rigidbody prb = p.GetComponent<Rigidbody>();
            prb.AddForce((spawn.up * forceThrow) + (spawn.forward * (forceThrow / 4)), ForceMode.Impulse);
            drop = true;
        }
    }
}
