using System.Collections;
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

    public bool HitObject = false;
    public bool IsActive;

    public bool DestroyOnHit;
    private bool IsTurned;
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
            //Debug.Log(IsActive);
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
                if (AttachedMove.AttachedExemon.GetComponent<BattleExemon>().TurnedAround)
                {
                    
                    AttachedMove.ResolveHitExemon(exemon, gameObject, ForceDirection.CustomAngle, AttachedMove.Damage, AttachedMove.StunTime, 0, 0, -Force, -Angle);
                    HitObject = true;
                }
                else
                {
                    AttachedMove.ResolveHitExemon(exemon, gameObject, ForceDirection.CustomAngle, AttachedMove.Damage, AttachedMove.StunTime, 0, 0, Force, Angle);
                    HitObject = true;
                }
            }

            AttachedMove.HitDelay = AttachedMove.DelayTime;
        }

        
        
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<BattleExemon>() && collider.gameObject != AttachedMove.AttachedExemon && IsActive)
        {

            CurrentCollisions.Add(collider.gameObject);

        }
        if (collider.gameObject.GetComponent<HitBox>())
        {
            
        }
            
        if(collider.gameObject.GetComponent<Projectile>())
        {

        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<BattleExemon>() && collider.gameObject != AttachedMove.AttachedExemon && IsActive)
        {

            CurrentCollisions.Remove(collider.gameObject);

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
 