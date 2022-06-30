using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    [HideInInspector] public AudioSource audioSource;
    public float beatTempo;
    public int length;

    [HideInInspector] public bool isFinished;
    // Start is called before the first frame update
    void Awake()
    {
        isFinished = false;
        beatTempo /= 60f;
        beatTempo = 1 / beatTempo;

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((int)audioSource.time == length - 2) isFinished = true;
    }
}
