using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    public float Accuracy;
    public float power;
    public float chargeTime;
    public float hitTime;
    public int APCost;
    public ElementalType type;
    public int MoveID;
    public float hp;
    public GameObject Projectile;
    public bool ranged;
    public float PracticalityDamageModifyer;
    public bool canMove;
    public GameObject AttachedExemon;

    public bool canExitAttack;
    public float lockedTime;
    public float startDelay;
    public float movementDelay;

    public float ProjectileForce;
    public float ProjectileAngle;
    public int ProjectileAmount;
    public Transform ProjectileSpawn;
    public float ProjectileDelay;

    public float DelayTime;
    public float shotsLeft;

    public float moveTime;

    public float MaxAimAngle;
    public float MinAimAngle; //TODO: IMPLEMENT AIMING SYSTEM AND have the min and max angles the player can fire be move dependant; 

    public List<HitBoxForce> hitBoxes;

    

    public void Update()
    {
        moveTime += Time.deltaTime;
    }
    public void ResolveHit(BattleExemon exemon, float stunDuration)
    {
        Debug.Log(exemon);
        exemon.finishedAttack = true;
        exemon.TakeDamage(power);
        exemon.ApplyStun(stunDuration);
    }
    

    public void AssignExemonHitBoxes()
    {
        
    }

    public void PowerUpMove()
    {

    }


    
    public virtual void LaunchRangedAttack()
    {

        
    } 
    public virtual void InitiateAttack()
    {
        shotsLeft = ProjectileAmount;
    }



}
 
public enum moveType
{
    Attack,
    Enchant,
    Affliction,

}


