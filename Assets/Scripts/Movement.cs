using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource;
    
    [SerializeField] float mainThrust = 700f;
    [SerializeField] float rotationThrust = 120f;
    [SerializeField] AudioClip thrustAudio;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftEngineParticles;
    [SerializeField] ParticleSystem rightEngineParticles;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.W))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }
 

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotate();
        }
    }

    void RotateLeft()
    {
        if (!rightEngineParticles.isPlaying)
        {
            rightEngineParticles.Play();
        }
        ApplyRotation(rotationThrust);
    }

    void RotateRight()
    {
        if (!leftEngineParticles.isPlaying)
        {
            leftEngineParticles.Play();
        }
        ApplyRotation(-rotationThrust);
    }

    void StopRotate()
    {
        leftEngineParticles.Stop();
        rightEngineParticles.Stop();
    }

    void ApplyRotation(float thisFrameRotation)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * thisFrameRotation * Time.deltaTime);
        rb.freezeRotation = false;
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(thrustAudio);
        }
        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }

    void StopThrusting()
    {
        mainEngineParticles.Stop();
        audioSource.Stop();
    }
}
