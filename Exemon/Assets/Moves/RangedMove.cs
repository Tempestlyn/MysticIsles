﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedMove : Move
{
    public GameObject ProjectileSpawn;
    public List<TimesToAddForce> TimesToAddForces;
    public List<ProjectileShootData> ProjectileData;
    public Vector2 target = new Vector2(0, 0);
    private bool AimLocked;
    private Vector2 AimLockedPosition = new Vector2(0,0);
    public List<AimLockedTime> AimLockedTimes;
    private bool AIControlled;

    private GameObject MoveRotation;
    private List<GameObject> InstantiatedProjectiles = new List<GameObject>();

    [System.Serializable]
    public struct AimLockedTime { public float TimeStart; public float Duration; }

    [System.Serializable]
    public struct TimesToAddForce { public int ProjectileIndex; public float time; public SpawnDirection projectileDirection; public float force; public bool doesRepeat; public int repeats; public bool MustBeStationary; }

    [System.Serializable]
    public class ProjectileShootData { public int ProjectileIndex; public GameObject projectile; public float TimeToShoot; public SpawnDirection projectileDirection; public float force; public float forceAngle; public float LifeSpan; public float ChanceToSpawn;



        public float IncreaseChanceToSpawn(float value)
        {
            ChanceToSpawn += value;

            return ChanceToSpawn;
        }
        
         }

    // Start is called before the first frame update
    void Start()
    {

        AIControlled = AttachedExemon.gameObject.GetComponent<EnemyBattleAI>();

        if (ProjectileSpawn == null)
        {
            ProjectileSpawn = AttachedExemon.GetComponent<BattleExemon>().MoveSpawn;
            
        }


        foreach (LevelingValues levelingValues in LevelingValues)
        {
            if (levelingValues.levelingType == LevelingType.MoveSpawnChance)
            {
                var value = ReturnLevelValue(levelingValues);
                for (int i = 0; i< ProjectileData.Count; i++)
                {
                    if (ProjectileData[i].ProjectileIndex == levelingValues.ProjectileIndex)
                    {
                        var remainder = ProjectileData[i].IncreaseChanceToSpawn(value) - 100;

                        if (remainder > 0)
                        {
                            value = remainder;
                            
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }

        if (MoveRotation == null)
        {
            MoveRotation = AttachedExemon.GetComponent<BattleExemon>().MoveRotation;
        }
        foreach(ProjectileShootData projectileShootData in ProjectileData)
        {
            var projectile = projectileShootData.projectile.GetComponent<Projectile>();
            projectile.Index = projectileShootData.ProjectileIndex;
            projectile.Force = projectileShootData.force;
            projectile.ForceAngle = projectileShootData.forceAngle;
            projectile.LifeSpan = projectileShootData.LifeSpan;
            StartCoroutine(Shoot(projectileShootData));

        }
        foreach(AimLockedTime aimLockedTime in AimLockedTimes)
        {
            StartCoroutine(LockAim(aimLockedTime));
        }

        foreach(TimesToAddForce timesToAddForce in TimesToAddForces)
        {
            StartCoroutine(ApplyForceToProjectile(timesToAddForce));
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
        }

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



        if (AimLocked)
        {
            target = AimLockedPosition;

        }
        else if(AIControlled)
        {
            target = BattleScene.BattleCam.ScreenToWorldPoint(new Vector2(AttachedExemon.GetComponent<BattleExemon>().enemyExemon.transform.position.x, AttachedExemon.GetComponent<BattleExemon>().enemyExemon.transform.position.y));
        }
        else
        {
            target = BattleScene.BattleCam.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        }
    }



    IEnumerator LockAim(AimLockedTime aimLockedTime)
    {
        yield return new WaitForSeconds(aimLockedTime.TimeStart);

        AimLocked = true;
        
        AimLockedPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        yield return new WaitForSeconds(aimLockedTime.Duration);
        AimLocked = false;

    }

    IEnumerator Shoot(ProjectileShootData shootData)
    {
        //Debug.Log(shootData.projectile);
        if (Random.value < (shootData.ChanceToSpawn / 100)){
            
            yield return new WaitForSeconds(shootData.TimeToShoot);

            if (shootData.projectileDirection == SpawnDirection.Mouse)
            {
               

                Vector2 myPos = ProjectileSpawn.transform.position;
                Vector2 difference = target - myPos;

                float sign = (target.y < myPos.y) ? -1.0f : 1.0f;
                var baseAngle = Vector2.Angle(Vector2.right, difference) * sign;

                if ((baseAngle < MinAimAngle && baseAngle > (-180 + MinAimAngle)))
                {
                    if (BattleScene.BattleCam.ScreenToWorldPoint(Input.mousePosition).x > AttachedExemon.transform.position.x )
                    {
                        baseAngle = MinAimAngle;
                    }
                    else
                    {
                        baseAngle = -180 + MinAimAngle;
                    }
                }
                else if(((baseAngle > MaxAimAngle && baseAngle < (180 - MaxAimAngle))))
                {
                    if (BattleScene.BattleCam.ScreenToWorldPoint(Input.mousePosition).x > AttachedExemon.transform.position.x)
                    {
                        baseAngle = MaxAimAngle;
                    }
                    else
                    {
                        baseAngle = 180 - MaxAimAngle;
                    }
                }
                


                baseAngle += (Random.Range(-Accuracy, Accuracy));




                float xcomponent = Mathf.Cos(baseAngle * Mathf.PI / 180) * shootData.force;
                float ycomponent = Mathf.Sin(baseAngle * Mathf.PI / 180) * shootData.force;

                var projectile = Instantiate(shootData.projectile, ProjectileSpawn.transform);
                projectile.gameObject.transform.parent = null;
                InstantiatedProjectiles.Add(projectile);
                projectile.GetComponent<Rigidbody2D>().AddForce(new Vector2(xcomponent, ycomponent));
                projectile.GetComponent<Projectile>().Index = shootData.ProjectileIndex;
                projectile.GetComponent<Projectile>().controllingMove = this;
                projectile.GetComponent<Projectile>().controllingExemon = AttachedExemon;
                projectile.GetComponent<Projectile>().damage = Damage;

            }
            if (shootData.projectileDirection == SpawnDirection.Up)
            {
                var projectile = Instantiate(shootData.projectile, ProjectileSpawn.transform);
                projectile.gameObject.transform.parent = null;
                InstantiatedProjectiles.Add(projectile);
                projectile.GetComponent<Rigidbody2D>().AddForce(transform.up * shootData.force);
                projectile.GetComponent<Projectile>().controllingMove = this;
                projectile.GetComponent<Projectile>().controllingExemon = AttachedExemon;
                projectile.GetComponent<Projectile>().damage = Damage;
            }
        }

    }

    IEnumerator ApplyForceToProjectile(TimesToAddForce forceData)
    {
        var startPosition = new Vector2(0, 0);
        if (forceData.MustBeStationary)
        {
            startPosition = AttachedExemon.transform.position;
        }
        yield return new WaitForSeconds(forceData.time);

        if (!forceData.MustBeStationary || (forceData.MustBeStationary && (Vector2.Distance(startPosition, AttachedExemon.transform.position) < 2)))
        {
            if (forceData.projectileDirection == SpawnDirection.Mouse)
            {


                foreach (GameObject projectile in InstantiatedProjectiles)
                {
                    var projectileData = projectile.GetComponent<Projectile>();
                    if (projectileData.Index == forceData.ProjectileIndex)
                    {
                        

                        Debug.Log(target);
                        Vector2 position = projectile.gameObject.transform.position;
                        Vector2 difference = target - position;
                        float sign = (target.y < position.y) ? -1.0f : 1.0f;
                        var baseAngle = Vector2.Angle(Vector2.right, difference) * sign;
                        baseAngle += (Random.Range(-Accuracy, Accuracy));
                        float xcomponent = Mathf.Cos(baseAngle * Mathf.PI / 180) * forceData.force;
                        float ycomponent = Mathf.Sin(baseAngle * Mathf.PI / 180) * forceData.force;

                        projectile.GetComponent<Rigidbody2D>().AddForce(new Vector2(xcomponent, ycomponent));
                    }
                    else if (forceData.ProjectileIndex == -1)
                    {
                        //Make EVERY projectile change direction;
                    }

                }

            }
            if (forceData.doesRepeat && forceData.repeats != 0)
            {
                forceData.repeats -= 1;
                StartCoroutine(ApplyForceToProjectile(forceData));
            }
        }
        
    }

    public override void InitiateAttack()
    {

    }

    public enum SpawnDirection
    {
        Mouse,
        Up,
        Down,
        MousePositionX

    }
    
}
