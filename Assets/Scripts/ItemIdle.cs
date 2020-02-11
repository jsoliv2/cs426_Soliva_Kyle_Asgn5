using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIdle : MonoBehaviour
{
    private bool canRotate = true;

    // Start is called before the first frame update
    Animation idleAnim;
    void Start()
    {
        idleAnim = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canRotate)
        {
            idleAnim.Play();
        }
        else
        {
            idleAnim.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player") // If a player character is in range to pick up the PC part
        {
            canRotate = false;
            transform.parent = other.transform;
            Debug.Log("Player found " + this.gameObject.ToString());
        }
        else
        {
            if(other.name == "Nest")
            {
                Debug.Log("Player brought " + this.gameObject.ToString() + " to the nest");
                Destroy(this.gameObject);
            }
        }
    }


}
