using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomChirps : MonoBehaviour
{
    AudioSource src;
    int rng;

    // Start is called before the first frame update
    void Start()
    {
        src = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        rng = Random.Range(0, 180);
        if(rng == 0)
        {
            src.Play();
        }
    }
}
