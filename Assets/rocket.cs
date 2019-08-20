﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody=GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if(Input.GetKey(KeyCode.Space)) //thrust
        {
            rigidBody.AddRelativeForce(Vector3.up);
        }
       
        if(Input.GetKey(KeyCode.A)) //left
        {
            transform.Rotate(Vector3.forward);
        }
        else if(Input.GetKey(KeyCode.D))    //right
        {
            transform.Rotate(-Vector3.forward);
        }
       
        
    }
}