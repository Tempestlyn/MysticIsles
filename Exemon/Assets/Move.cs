using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    public float accuracy;
    public float power;
    public float chargeTime;
    public float hitTime;
    public int APCost;
    public ElementalType type;
    public int MoveID;
    public float hp;
    public GameObject projectile;
    public bool ranged;
    public float PracticalityDamageModifyer;
    public bool mustStandStill;
    public GameObject AttachedExemon;

    public List<HitBoxForce> hitBoxes;


    
    private void Update()
    {

    }

    public void ResolveHit(BattleExemon exemon)
    {
        exemon.HitTest();
    }


    public void AssignExemonHitBoxes()
    {
        
    }


}

public enum moveType
{
    Attack,
    Enchant,
    Affliction,

}


