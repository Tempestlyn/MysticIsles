using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed;
    private Rigidbody myRigidbody;
    public float Gravity;
    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        Movement();
        myRigidbody.AddRelativeForce(Vector3.down * Gravity);
    }

    void Movement()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");
        Vector3 playerMovement = new Vector3(hor, 0, ver).normalized * Speed * Time.deltaTime;
        transform.Translate(playerMovement, Space.Self);
    }
}