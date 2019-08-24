using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource audioSource;
    [SerializeField]float rcsThrust = 100f;//reaction control system
    [SerializeField]float mainThrust = 100f;//reaction control system
    // Start is called before the first frame update
    void Start()
    {
        rigidBody=GetComponent<Rigidbody>();
        audioSource= GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
    }
    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space)) //thrust
        {
            rigidBody.AddRelativeForce(Vector3.up*mainThrust);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }
    private void Rotate()
    {
        rigidBody.freezeRotation = true;  //take manual control of rotation
      
        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A)) //left
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))    //right
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        rigidBody.freezeRotation = false;//resume physics control rotation
    }


  
}
