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

    public List<BoxCollider2D> colliders;

    private void Update()
    {
        
    }

    private void ResolveHit(Move move)
    {

    }




}

public enum moveType
{
    Attack,
    Enchant,
    Affliction,

}


