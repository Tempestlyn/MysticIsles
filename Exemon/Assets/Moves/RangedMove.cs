using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedMove : Move
{
    public int ProjectileAmount;
    public List<float> ForceStrengths;
    public Transform ProjectileSpawn;
    public float ProjectileDelay;
    public List<TimesToAddForce> TimesToAddForces;
    public List<ProjectileShootData> Projectiles;

    [System.Serializable]
    public struct TimesToAddForce { public int ProjectileIndex; public float time; public float ForceSelectionIndex; public Direction projectileDirection; }

    [System.Serializable]
    public struct ProjectileShootData { public int ProjectileIndex; public GameObject projectile; public float TimeToShoot; public float force; }

    // Start is called before the first frame update
    void Start()
    {


        foreach(ProjectileShootData projectileShootData in Projectiles)
        {
            projectileShootData.projectile.GetComponent<Projectile>().Index = projectileShootData.ProjectileIndex;
            StartCoroutine(Shoot(projectileShootData));
        }


    }

    // Update is called once per frame
    void Update()
    {
        
        moveTime += Time.deltaTime;
        foreach (Vector2 time in NoMovementTimes)
        {
            if (moveTime < time[0] || moveTime > time[1])
            {
                CanMove = true;
            }
        }//TODO: CAN MOVE WILL BE BASED OFF OF List OF VECTOR2s that indicates the times the player can/can't move

        foreach (Vector2 time in lockedTimes)
        {
            if (moveTime >= time[0] && moveTime <= time[1])
            {
                canExitAttack = false;
            }
            else
            {
                canExitAttack = true;
            }

        }

        if (moveTime >= MaxMoveTime || AttachedExemon.GetComponent<BattleExemon>().ActiveMove != this)
        {
            Destroy(gameObject);
        }


        DelayTime -= Time.deltaTime;
    }

    IEnumerator Shoot(ProjectileShootData shootData)
    {
        yield return new WaitForSeconds(shootData.TimeToShoot);

        Vector2 target = BattleScene.BattleCam.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        Vector2 myPos = ProjectileSpawn.transform.position;

        Vector2 difference = target - myPos;
        float sign = (target.y < myPos.y) ? -1.0f : 1.0f;
        var baseAngle = Vector2.Angle(Vector2.right, difference) * sign;
        baseAngle += (Random.Range(-Accuracy, Accuracy));
        float xcomponent = Mathf.Cos(baseAngle * Mathf.PI / 180) * shootData.force;
        float ycomponent = Mathf.Sin(baseAngle * Mathf.PI / 180) * shootData.force;

        var projectile = Instantiate(shootData.projectile, ProjectileSpawn.transform);
        projectile.gameObject.transform.parent = null;

        projectile.GetComponent<Rigidbody2D>().AddForce(new Vector2(xcomponent, ycomponent));
        projectile.GetComponent<Projectile>().controllingMove = this;
        projectile.GetComponent<Projectile>().controllingExemon = AttachedExemon;
        projectile.GetComponent<Projectile>().damage = Damage;

    }

    public override void InitiateAttack()
    {
        shotsLeft = ProjectileAmount;
    }

    public void ApplyForceToProjectile( )
    {

    }
    public enum Direction
    {
        Mouse,
        Up

    }
    
}
