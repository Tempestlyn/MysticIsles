using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrapBackground : MonoBehaviour
{

    private BoxCollider boxCollider;
    private Rigidbody rb;
    private float width;
    public float speed;
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();

        width = boxCollider.size.x;
        rb.velocity = new Vector2(speed, 0);

    }

    void Update()
    {
        if (transform.position.x < -width -20)
        {
            Reposition();
        }
        
    }

    private void Reposition()
    {
        Vector3 vector = new Vector3((transform.position.x + width * 4) - 1, transform.position.y, transform.position.z);
        transform.position = vector;
    }
}