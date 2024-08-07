using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource;
    [SerializeField] ParticleSystem mainRocketThrust;
    [SerializeField] ParticleSystem leftRocketThrust;
    [SerializeField] ParticleSystem rightRocketThrust;

    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        ApplyThrust();
        ApplyRotation();
    }

    void ApplyThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void StopThrusting()
    {
        audioSource.Stop();
        mainRocketThrust.Stop();
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
        if (!mainRocketThrust.isPlaying)
        {
            mainRocketThrust.Play();
        }
    }

    void ApplyRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if(Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotation();
        }

    }

    void StopRotation()
    {
        leftRocketThrust.Stop();
        rightRocketThrust.Stop();
    }

    void RotateRight()
    {
        DoRotation(-rotationThrust);
        if (!rightRocketThrust.isPlaying)
        {
            rightRocketThrust.Play();
        }
    }

    void RotateLeft()
    {
        DoRotation(rotationThrust);
        if (!leftRocketThrust.isPlaying)
        {
            leftRocketThrust.Play();
        }
    }

    void DoRotation(float rotationThrust)
    {
        rb.freezeRotation = false;
        transform.Rotate(Vector3.forward * rotationThrust * Time.deltaTime);
        rb.freezeRotation = true;
    }
}
