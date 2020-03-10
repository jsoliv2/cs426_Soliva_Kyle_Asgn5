using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TackleObject : MonoBehaviour
{
    private Rigidbody rb;
    private AudioSource src;
    public float pushFactor = 1.0f; // How easily an object can be pushed
    public float portionRequirement = 50.0f; // How much speed percentage the player needs to move this object

    public GameObject explodeParticle;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        src = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position.x >= 200 || this.transform.position.x <= -200 || this.transform.position.z >= 200 || this.transform.position.z <= -200)
        {
            Vector3 pos = this.transform.position;
            for (int i = 0; i < 10; ++i)
            {
                GameObject clone = Instantiate(explodeParticle, pos, Random.rotation);
                clone.GetComponent<Rigidbody>().AddForce(transform.forward * portionRequirement * 60);
            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (other.gameObject.GetComponent<BirdMovement>().getSpeedPortion() >= portionRequirement)
            {
                rb.AddForce(other.transform.forward * other.gameObject.GetComponent<BirdMovement>().getSpeedPortion() * pushFactor);
                Debug.Log(other.gameObject.GetComponent<BirdMovement>().getSpeedPortion());
                src.Play();
            }
        }

        if(other.tag == "Barrier")
        {
            Vector3 pos = this.transform.position;
            for(int i = 0; i < 10; ++i)
            {
                GameObject clone = Instantiate(explodeParticle, pos, Random.rotation);
                clone.GetComponent<Rigidbody>().AddForce(transform.forward * portionRequirement * 60);
            }
            Destroy(gameObject);
        }
    }
}