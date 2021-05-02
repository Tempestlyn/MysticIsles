using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    public float Accuracy;
    public float Damage;
    public float StunTime;
    public float hitTime;
    public int APCost;
    public ElementalType type;
    public int MoveID;
    public float hp;
    public float PracticalityDamageModifyer;
 
    public GameObject AttachedExemon;

    

    public List<Vector2> lockedTimes;
    public List<Vector2> NoMovementTimes;
    public float startDelay;
    
    

    public float DelayTime;
    public float shotsLeft;
    public float MaxMoveTime;

    


    public float MaxAimAngle;
    public float MinAimAngle; //TODO: IMPLEMENT AIMING SYSTEM AND have the min and max angles the player can fire be move dependant; 

    [System.NonSerialized]
    public float HitDelay;
    public float moveTime = 0;
    public bool canExitAttack;
    public bool CanMove;

    public void Update()
    {
        moveTime += Time.deltaTime;
        
    }
    public void ResolveHit(BattleExemon exemon, float Damage, float stunDuration, float force, float forceAngle)
    {
        Debug.Log(exemon);
        exemon.TakeDamage(Damage);
        exemon.ApplyStun(stunDuration);
        ApplyForce(exemon.gameObject, force, forceAngle);
    }

    public void ApplyForce(GameObject exemon, float force, float forceAngle)
    {
        //Force = force;
        //InitialMovement = true;
        //Debug.Log(Force);
        float xcomponent = Mathf.Cos(forceAngle * Mathf.PI / 180) * force;
        float ycomponent = Mathf.Sin(forceAngle * Mathf.PI / 180) * force;
        exemon.GetComponent<Rigidbody2D>().AddForce(new Vector2(xcomponent, ycomponent));
        Debug.Log("Test");


    }



    public void PowerUpMove()
    {

    }


    
    public virtual void LaunchRangedAttack()
    {

        
    } 
    public virtual void InitiateAttack()
    {

    }



}
 
public enum moveType
{
    Attack,
    Enchant,
    Affliction,

}


