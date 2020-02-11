using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestGoal : MonoBehaviour
{
    public UIListScript list;

    // Start is called before the first frame update
    void Start()
    {
        
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
        }
        else if(other.name == "Harddrive(Clone)")
        {
            list.collectHardDrive();
        }
        else if(other.name == "Motherboard(Clone)")
        {
            list.collectMotherboard();
        }
        else if(other.name == "Power(Clone)")
        {
            list.collectPower();
        }
        else
        {
            if(other.name == "Ram Stick(Clone)")
            {
                list.collectRamStick();
            }
        }
    }
}
