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
    public GameObject Projectile;
    public bool ranged;
    public float PracticalityDamageModifyer;
    public bool mustStandStill;
    public GameObject AttachedExemon;

    public bool canExitAttack;

    public float ProjectileForce;
    public float ProjectileAngle;
    public int ProjectileAmount;
    public Transform ProjectileSpawn;
    public float ProjectileDelay;

    private float DelayTime;
    private float shotsLeft;
   

    public List<HitBoxForce> hitBoxes;

    

    public void Update()
    {
        
        if (shotsLeft > 0 && AttachedExemon && AttachedExemon.GetComponent<BattleExemon>().ActiveMove == this)
        {
            
            if (DelayTime <= 0)
            {
                Vector3 worldPoint = BattleScene.BattleCam.ScreenToWorldPoint(Input.mousePosition);
                Vector2 worldPoint2d = new Vector2(worldPoint.x, worldPoint.y);

                Vector3 targetDir = ProjectileSpawn.position - worldPoint;
                float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
                var projectile = Instantiate(Projectile, ProjectileSpawn);
                projectile.GetComponent<Projectile>().ApplyForce(ProjectileForce, angle);

                DelayTime += ProjectileDelay;
                
            }  
        }
        else if (shotsLeft <= 0)
        {
            AttachedExemon.GetComponent<BattleExemon>().EndAttack();
        }
        DelayTime -= Time.deltaTime;
    }
    public void ResolveHit(BattleExemon exemon, float stunDuration)
    {
        //Debug.Log(stunDuration);
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
        shotsLeft += ProjectileAmount;
        
    } 



}
 
public enum moveType
{
    Attack,
    Enchant,
    Affliction,

}


