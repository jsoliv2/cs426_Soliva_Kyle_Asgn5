using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestGoal : MonoBehaviour
{
    public UIListScript list;
    private AudioSource src;

    // Start is called before the first frame update
    void Start()
    {
        src = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Chip(Clone)")
        {
            list.collectChip();
            src.Play();
        }
        else if(other.name == "Harddrive(Clone)")
        {
            list.collectHardDrive();
            src.Play();
        }
        else if(other.name == "Motherboard(Clone)")
        {
            list.collectMotherboard();
            src.Play();
        }
        else if(other.name == "Power(Clone)")
        {
            list.collectPower();
            src.Play();
        }
        else
        {
            if(other.name == "Ram Stick(Clone)")
            {
                list.collectRamStick();
                src.Play();
            }
        }
    }
}
