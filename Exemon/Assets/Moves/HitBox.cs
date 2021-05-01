using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public BoxCollider Collider;
    public float Force;
    public float Angle;
    public int Damage;
    public float StartTime;
    public float EndTime;
    public Vector2 StartPosition;
    public Vector2 EndPosition;

    public float DamageDifferencial;
    public float ForceDifferencial;

    private bool HitObject;
    public bool IsActive;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider collider)
    {
        if (IsActive)
        {
        

        }
    }
}
