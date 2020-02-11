using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ObjectSpawn : NetworkBehaviour
{
    public GameObject[] pCParts;
    public GameObject[] StairsSpawn;
    public GameObject[] RocksSpawn;
    public GameObject[] PondSpawn;
    public GameObject[] AirSpawn;
    public GameObject[] TreeSpawn;

    // Start is called before the first frame update
    void Start()
    {
        if(!isServer)
        {
            return;
        }
        Instantiate(pCParts[0], StairsSpawn[Random.Range(0, 2)].transform.position, this.transform.rotation);
        Instantiate(pCParts[1], PondSpawn[Random.Range(0, 2)].transform.position, this.transform.rotation);
        Instantiate(pCParts[2], RocksSpawn[Random.Range(0, 3)].transform.position, this.transform.rotation);
        Instantiate(pCParts[3], AirSpawn[Random.Range(0, 3)].transform.position, this.transform.rotation);
        Instantiate(pCParts[4], TreeSpawn[Random.Range(0, 3)].transform.position, this.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isServer)
        {
            return;
        }
    }
}
