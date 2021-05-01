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
    public bool ranged;
    public float PracticalityDamageModifyer;
    public bool CanMove;
    public GameObject AttachedExemon;

    public bool canExitAttack;

    public List<Vector2> lockedTimes;
    public float startDelay;
    public float movementDelay;
    public float HitDelay;

    public float DelayTime;
    public float shotsLeft;
    public float maxmoveTime;

    public float moveTime;


    public float MaxAimAngle;
    public float MinAimAngle; //TODO: IMPLEMENT AIMING SYSTEM AND have the min and max angles the player can fire be move dependant; 


    

    public void Update()
    {
        moveTime += Time.deltaTime;
        CanMove = moveTime >= movementDelay;//TODO: CAN MOVE WILL BE BASED OFF OF List OF VECTOR2s that indicates the times the player can/can't move
    }
    public void ResolveHit(BattleExemon exemon, float stunDuration)
    {
        Debug.Log(exemon);
        exemon.TakeDamage(power);
        exemon.ApplyStun(stunDuration);
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


