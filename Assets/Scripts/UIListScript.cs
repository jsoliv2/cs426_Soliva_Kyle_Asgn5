using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class UIListScript : NetworkBehaviour
{   [SyncVar]
    private bool isChipCollected = false;
    [SyncVar]
    private bool isHardDriveCollected = false;
    [SyncVar]
    private bool isMotherboardCollected = false;
    [SyncVar]
    private bool isPowerCollected = false;
    [SyncVar]
    private bool isRamStickCollected = false;

    public Text todoList;
    public Text timeLeft;

    [SyncVar]
    private int framesPerSecond = 60;
    [SyncVar]
    private int frameCounter = 0;
    [SyncVar]
    public int timeToBeat = 180;

    [SyncVar]
    private bool isAWin = false;

    public void collectChip()
    {
        isChipCollected = true;
    }
    
    public void collectHardDrive()
    {
        isHardDriveCollected = true;
    }

    public void collectMotherboard()
    {
        isMotherboardCollected = true;
    }

    public void collectPower()
    {
        isPowerCollected = true;
    }

     public void collectRamStick()
    {
        isRamStickCollected = true;
    }
    private void displayList()
    {
        todoList.text = "TODO:\n";
        if (!isChipCollected)
        {
            todoList.text += "[ ] Computer Chip\n";
        }
        else
        {
            todoList.text += "[O] Computer Chip\n";
        }

        if (!isHardDriveCollected)
        {
            todoList.text += "[ ] Hard Drive\n";
        }
        else
        {
            todoList.text += "[O] Hard Drive\n";
        }

        if (!isMotherboardCollected)
        {
            todoList.text += "[ ] Motherboard\n";
        }
        else
        {
            todoList.text += "[O] Motherboard\n";
        }

        if (!isPowerCollected)
        {
            todoList.text += "[ ] Power\n";
        }
        else
        {
            todoList.text += "[O] Power\n";
        }

        if (!isRamStickCollected)
        {
            todoList.text += "[ ] RAM Stick\n";
        }
        else
        {
            todoList.text += "[O] RAM Stick\n";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isChipCollected && isHardDriveCollected && isMotherboardCollected && isPowerCollected && isRamStickCollected)
        {
            if(timeToBeat > 0)
            {
                isAWin = true;
                todoList.text = "YOU WIN!";
            }
            else
            {
                todoList.text = "You lost...";
            }
        }
        else
        {
            displayList();
        }

        if(!isAWin)
        {
            frameCounter++;
            if (frameCounter == framesPerSecond)
            {
                frameCounter = 0;
                if (timeToBeat > 0)
                {
                    timeToBeat--;
                }
            }
            timeLeft.text = "TIME LEFT: " + timeToBeat;
        }
    }
}
