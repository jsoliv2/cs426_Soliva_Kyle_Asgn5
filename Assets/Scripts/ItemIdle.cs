using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIdle : MonoBehaviour
{

    public ScoreHandler score;
    // Start is called before the first frame update
    Animation idleAnim;
    void Start()
    {
        idleAnim = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        idleAnim.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player") // If a player character is in range to pick up the PC part
        {
            Debug.Log("Player found " + this.gameObject.ToString());
            Destroy(this.gameObject);
        }
    }


}
