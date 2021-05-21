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
    public float AIRange;
    public GameObject AttachedExemon;
    public GameObject ProjectileSpawn;


    public List<Vector2> lockedTimes;
    public List<Vector2> NoMovementTimes;
    public List<Vector2> MustBeStationaryTimes;
    public List<Vector2> NoTurnTimes;



    [SerializeField]
    public List<LevelingValues> LevelingValues;
    
    

    public float DelayTime;
    public float MaxMoveTime;



    public float MaxStationaryMoveDistance;
    public float MaxAimAngle;
    public float MinAimAngle;
    public float AimHeightOffset;
    public float MaxDistanceOffset;


    [System.NonSerialized]
    public float HitDelay;
    [System.NonSerialized]
    public float moveTime = 0;
    [System.NonSerialized]
    public bool canExitAttack;
    [System.NonSerialized]
    public bool CanMove;
    [System.NonSerialized]
    public Vector2 StartPosition;
    [System.NonSerialized]
    public bool CanTurn;
    [System.NonSerialized]
    public bool IsStationary;

    public float MoveExperience;

    public void Update()
    {
        moveTime += Time.deltaTime;
        
    }
    public void ResolveHitExemon(GameObject target, GameObject CollidingEntity, float Damage, float stunDuration, float force, float forceAngle)
    {
        if (target.GetComponent<BattleExemon>())
        {
            var exemon = target.GetComponent<BattleExemon>();
            exemon.TakeDamage(Damage);
            exemon.ApplyStun(stunDuration);
            ApplyForce(exemon.gameObject, CollidingEntity.GetComponent<Rigidbody2D>().velocity, force, forceAngle);
        }
        else if (target.GetComponent<Projectile>())
        {

        }
    }

    public void ResolveHitProjectile(Projectile projectile, GameObject CollidingEntity, float Damage, float force, float forceAngle)
    {
        
        projectile.TakeDamage(Damage);
        ApplyForce(projectile.gameObject, CollidingEntity.GetComponent<Rigidbody2D>().velocity, force, forceAngle);
    }

    public void ResolveHitHitbox(HitBox hitBox, float Damage)
    {

    }


    public void ApplyForce(GameObject target, Vector2 CollidingVelocity, float force, float forceAngle)
    {
        //Force = force;
        //InitialMovement = true;
        //Debug.Log(Force);
        float xcomponent = Mathf.Cos(forceAngle * Mathf.PI / 180) * force;
        if (CollidingVelocity.x < 0)
        {
            xcomponent = -Mathf.Cos(forceAngle * Mathf.PI / 180) * force;
        }


        float ycomponent = Mathf.Sin(forceAngle * Mathf.PI / 180) * force;
        if (CollidingVelocity.y < 0)
        {
            ycomponent = -Mathf.Sin(forceAngle * Mathf.PI / 180) * force;
        }
        target.GetComponent<Rigidbody2D>().velocity = (new Vector2(xcomponent, ycomponent));



    } 

    public void ApplyForceHitbox(GameObject target, float force, float forceAngle)
    {
        float xcomponent = Mathf.Cos(forceAngle * Mathf.PI / 180) * force;
        if (AttachedExemon.GetComponent<BattleExemon>().TurnedAround)
        {
            xcomponent = -Mathf.Cos(forceAngle * Mathf.PI / 180) * force;
        }
        float ycomponent = Mathf.Sin(forceAngle * Mathf.PI / 180) * force;
        target.GetComponent<Rigidbody2D>().velocity = (new Vector2(xcomponent, ycomponent));
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

    public float ReturnLevelValue(LevelingValues levelingValues)
    {
        return (levelingValues.MaxLevel * (((((-1 / (((levelingValues.MoveLearnSpeed / 1) * MoveExperience +1)))) + 1))));
    }

    public void LevelUpForce(float MoveExperience)
    {

    }

    public IEnumerator SetStationary(Vector2 StationaryValues)
    {
        yield return new WaitForSeconds(StationaryValues.x);

        IsStationary = true;

        StartPosition = AttachedExemon.transform.position;
        yield return new WaitForSeconds(StationaryValues.y);
        IsStationary = false;

    }

    public IEnumerator SetNoTurn(Vector2 noTurnTimes)
    {
        yield return new WaitForSeconds(noTurnTimes.x);

        CanTurn = false;

        
        yield return new WaitForSeconds(noTurnTimes.y);
        CanTurn = true;

    }
}
 
public enum DamageType
{
    Exemon,
    Projectile,
    HitBox,

}

public enum LevelingType
{
    MoveHealth,
    MoveSpawnForce,
    MoveSpawnChance
}


[System.Serializable]
public struct LevelingValues
{
    public float MaxLevel;
    public float MoveLearnSpeed;
    public LevelingType levelingType;
    public float ProjectileIndex;
}

