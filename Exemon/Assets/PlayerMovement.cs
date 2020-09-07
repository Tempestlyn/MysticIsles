using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform player;
    public float speed = 0.5f;
    public Rigidbody rigidbody;
    private bool canRotateCamera;
    public bool canMove;
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }
    void Update()
    {
        var movementVector = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.D))
        {
            movementVector = new Vector3(1, movementVector.y, movementVector.z);
        }
        if (Input.GetKey(KeyCode.W))
        {
            movementVector = new Vector3(movementVector.x, movementVector.y, 1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            movementVector = new Vector3(-1, movementVector.y, movementVector.z);
        }
        if (Input.GetKey(KeyCode.S))
        {
            movementVector = new Vector3(movementVector.x, movementVector.y, -1);
        }

 
        rigidbody.velocity = movementVector * speed;

        if (Input.GetKey(KeyCode.Mouse2))
        {
            canRotateCamera = true;
        }
        
    }
}
    enum Diraction
{
    North,
    East,
    South,
    West
}