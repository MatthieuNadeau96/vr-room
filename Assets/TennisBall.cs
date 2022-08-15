using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class TennisBall : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip impact;
    double ballVel = 0, highestVel = 0;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        audioSource.Stop();
        foreach (ContactPoint contact in collision.contacts)
        {
            UnityEngine.Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
        ballVel = collision.relativeVelocity.magnitude;

        if(ballVel > highestVel) { highestVel = ballVel; }

        float vol = Convert.ToSingle(ballVel / highestVel);

        audioSource.PlayOneShot(impact, vol);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
