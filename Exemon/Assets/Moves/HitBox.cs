﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public BoxCollider2D Collider;
    public float Force;
    public float Angle;
    public float Damage;
    public float StartTime;
    public float EndTime;
    public Vector2 StartPosition;
    public Vector2 EndPosition;
    public int hitBoxIndex;
    public float Health;

    public float DamageDifferencial;
    public float ForceDifferencial;
    public float StunDifferencial;

    public bool HitObject = false;
    public bool IsActive;

    public bool DestroyOnHit;
    public MeleeAttack AttachedMove;
    List<GameObject> CurrentCollisions = new List<GameObject>();

    void Start()
    {
        Collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (AttachedMove.AttachedExemon.GetComponent<BattleExemon>().ActiveMove != AttachedMove)
        {
            Debug.Log(IsActive);
            IsActive = false;
        }
        if (HitObject && DestroyOnHit)
        {
            return;
        }
        if (AttachedMove.HitDelay <= 0)
        {
            foreach (GameObject exemon in CurrentCollisions)
            {
                if (!exemon.GetComponent<BattleExemon>().TurnedAround)
                {
                    AttachedMove.ResolveHitExemon(exemon.GetComponent<BattleExemon>(), AttachedMove.Damage, AttachedMove.StunTime, -Force, -Angle);
                    HitObject = true;
                }
                else
                {
                    AttachedMove.ResolveHitExemon(exemon.GetComponent<BattleExemon>(), AttachedMove.Damage, AttachedMove.StunTime, Force, Angle);
                    HitObject = true;
                }
            }

            AttachedMove.HitDelay = AttachedMove.DelayTime;
        }

        
        
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<ExemonHitbox>() && collider.gameObject.GetComponent<ExemonHitbox>().battleExemon != AttachedMove.AttachedExemon && IsActive)
        {

            CurrentCollisions.Add(collider.gameObject.GetComponent<ExemonHitbox>().battleExemon.gameObject);

        }
        if (collider.gameObject.GetComponent<HitBox>())
        {
            
        }
            
        if(collider.gameObject.GetComponent<Projectile>())
        {

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ExemonHitbox>() && collision.gameObject.GetComponent<ExemonHitbox>().battleExemon != AttachedMove.AttachedExemon && IsActive)
        {

            CurrentCollisions.Remove(collision.gameObject.GetComponent<ExemonHitbox>().battleExemon.gameObject);

        }
    }


    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
 