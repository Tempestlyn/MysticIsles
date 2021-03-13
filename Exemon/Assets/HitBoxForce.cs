using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxForce : MonoBehaviour
{
    public float force;
    public float angle;
    public BoxCollider2D boxCollider;




    public void AddForceAtAngle(float force, float angle, Rigidbody2D exemon)
    {
        float xcomponent = Mathf.Cos(angle * Mathf.PI / 180) * force;
        float ycomponent = Mathf.Sin(angle * Mathf.PI / 180) * force;

        exemon.AddForce(new Vector2(xcomponent, ycomponent));
    }
}
