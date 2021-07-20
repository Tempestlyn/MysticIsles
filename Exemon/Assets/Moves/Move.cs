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
    public void ResolveHitExemon(GameObject target, GameObject CollidingEntity, ForceDirection forceDirection, float Damage, float stunDuration, float xcomponent, float ycomponent, float force, float forceAngle)
    {
        if (target.GetComponent<BattleExemon>())
        {
            var exemon = target.GetComponent<BattleExemon>();
            exemon.TakeDamage(Damage);
            if (stunDuration > 0)
            {
                exemon.ApplyStun(stunDuration);
            }


            if (!CollidingEntity.GetComponent<HitBox>())
            {
                ApplyForce(exemon.gameObject, CollidingEntity.GetComponent<Rigidbody2D>().velocity, forceDirection, force, forceAngle, xcomponent, ycomponent);
            }
            else
            {
                ApplyForceHitbox(exemon.gameObject, force, forceAngle);
            }
        }
        else if (target.GetComponent<Projectile>())
        {

        }
    }

    public void ResolveHitProjectile(Projectile projectile, GameObject CollidingEntity, ForceDirection forceDirection, float Damage, float xcomponent, float ycomponent, float force = 0, float forceAngle = 0)
    {
        
        projectile.TakeDamage(Damage);

        if (force > 0)
        {


            ApplyForce(projectile.gameObject, CollidingEntity.GetComponent<Rigidbody2D>().velocity, forceDirection, force, forceAngle, xcomponent, ycomponent);
        }
    }

    public void ResolveHitHitbox(HitBox hitBox, float Damage)
    {

    }


    public void ApplyForce(GameObject target, Vector2 CollidingVelocity, ForceDirection forceDirection, float force, float forceAngle, float Xcomponent, float Ycomponent)
    {
        //Force = force;
        //InitialMovement = true;
        //Debug.Log(Force);

        Debug.Log(force);

        if (forceDirection == ForceDirection.CustomAngle)
        {
            var xcomponent = Mathf.Cos(forceAngle * Mathf.PI / 180) * force;
            var ycomponent = Mathf.Sin(forceAngle * Mathf.PI / 180) * force;
            if (CollidingVelocity.x < 0)
            {
                xcomponent = -Mathf.Cos(forceAngle * Mathf.PI / 180) * force;
            }



            if (CollidingVelocity.y < 0)
            {
                //ycomponent = -Mathf.Sin(forceAngle * Mathf.PI / 180) * force;
            }

            target.GetComponent<Rigidbody2D>().AddForce(new Vector2(xcomponent, ycomponent), ForceMode2D.Impulse);
        }

        else if (forceDirection == ForceDirection.ImpactPoint)
        {
            target.GetComponent<Rigidbody2D>().AddForce(new Vector2(-Xcomponent, -Ycomponent) * force, ForceMode2D.Impulse);
        }



    } 

    public void ApplyForceHitbox(GameObject target, float force, float forceAngle)
    {
        float xcomponent = Mathf.Cos(forceAngle * Mathf.PI / 180) * force;
        float ycomponent = Mathf.Sin(forceAngle * Mathf.PI / 180) * force;
        target.GetComponent<Rigidbody2D>().AddForce(new Vector2(xcomponent, ycomponent), ForceMode2D.Impulse);
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

