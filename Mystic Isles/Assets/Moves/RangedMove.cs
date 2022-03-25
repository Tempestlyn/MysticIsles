﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedMove : Move
{
    //public GameObject ProjectileSpawn;
    public List<TimesToAddForce> TimesToAddForces;
    public List<ProjectileShootData> ProjectileData;
    

    public List<AimLockedTime> AimLockedTimes;
    private GameObject MoveRotation;
    private List<GameObject> InstantiatedProjectiles = new List<GameObject>();



    [System.Serializable]
    public struct TimesToAddForce { public int ProjectileIndex; public float time; public SpawnDirection projectileDirection; public float force; public bool doesRepeat; public int repeats; public bool MustBeStationary; }

    [System.Serializable]
    public class ProjectileShootData { public int ProjectileIndex; public GameObject projectile; public float TimeToShoot; public SpawnDirection projectileDirection; public ForceDirection forceDirection; public float force; public float forceAngle; public float LifeSpan; public float ChanceToSpawn;



        public float IncreaseChanceToSpawn(float value)
        {
            ChanceToSpawn += value;

            return ChanceToSpawn;
        }
        
         }

    // Start is called before the first frame update
    void Start()
    {
        StartPosition = AttachedExemon.transform.position;
        if (ProjectileSpawn == null)
        {
            ProjectileSpawn = AttachedExemon.GetComponent<BattleEntity>().MoveSpawn;
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
            MoveRotation = AttachedExemon.GetComponent<BattleEntity>().MoveRotation;
        }
        foreach(ProjectileShootData projectileShootData in ProjectileData)
        {
            var projectile = projectileShootData.projectile.GetComponent<Projectile>();
            projectile.Index = projectileShootData.ProjectileIndex;
            //projectile.Force = projectileShootData.force;
            projectile.ForceAngle = projectileShootData.forceAngle;
            projectile.LifeSpan = projectileShootData.LifeSpan;
            projectile.forceDirection = projectileShootData.forceDirection;
            StartCoroutine(Shoot(projectileShootData));

        }
        foreach(AimLockedTime aimLockedTime in AimLockedTimes)
        {
            StartCoroutine(AttachedExemon.GetComponent<BattleEntity>().LockAim(aimLockedTime));
        }

        foreach(TimesToAddForce timesToAddForce in TimesToAddForces)
        {
            StartCoroutine(ApplyForceToProjectile(timesToAddForce));
        }

        foreach(Vector2 stationaryValues in MustBeStationaryTimes)
        {
            StartCoroutine(SetStationary(stationaryValues));
        }
        foreach (Vector2 noTurnTime in NoTurnTimes)
        {
            StartCoroutine(SetNoTurn(noTurnTime));
        }




    }

    // Update is called once per frame
    void Update()
    {
        if (IsStationary && MaxStationaryMoveDistance < Vector2.Distance(StartPosition, AttachedExemon.transform.position))
        {
            AttachedExemon.GetComponent<BattleEntity>().EndAttack();
        }

        CanMove = false;
        moveTime += Time.deltaTime;
        if (NoMovementTimes.Count > 0)
        {
            foreach (Vector2 time in NoMovementTimes)
            {
                if (moveTime < time[0] || moveTime > time[1])
                {
                    CanMove = true;
                }
            }
        }
        else
        {
            CanMove = true;
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

        if (moveTime >= MaxMoveTime || AttachedExemon.GetComponent<BattleEntity>().ActiveMove != this)
        {
            Destroy(gameObject);
        }


        DelayTime -= Time.deltaTime;

    }


    IEnumerator Shoot(ProjectileShootData shootData)
    {
        //Debug.Log(shootData.projectile);
        if (Random.value < (shootData.ChanceToSpawn / 100)){
            
            yield return new WaitForSeconds(shootData.TimeToShoot);

            var projectileObject = Instantiate(shootData.projectile, ProjectileSpawn.transform);
            projectileObject.gameObject.transform.parent = null;
            InstantiatedProjectiles.Add(projectileObject);
            var projectile = projectileObject.GetComponent<Projectile>();
            projectile.controllingMove = this;
            projectile.controllingExemon = AttachedExemon;
            projectile.damage = Damage;
            projectile.forceDirection = shootData.forceDirection;
            projectile.ForceAngle = shootData.forceAngle;
            projectile.StartPosition = projectile.transform.position;
            if (shootData.projectileDirection == SpawnDirection.Mouse)
            {
               

                Vector2 myPos = ProjectileSpawn.transform.position;
                var target = AttachedExemon.GetComponent<BattleEntity>().target;
                Vector2 difference = target - myPos;
                
                float sign = (AttachedExemon.GetComponent<BattleEntity>().target.y < myPos.y) ? -1.0f : 1.0f;
                var baseAngle = Vector2.Angle(Vector2.right, difference) * sign;

                var distance = Mathf.Abs(AttachedExemon.transform.position.x - target.x);
                if (distance > MaxDistanceOffset)
                {
                    distance = MaxDistanceOffset;
                }
                if (target.x > AttachedExemon.transform.position.x)
                {

                    baseAngle += distance * AimHeightOffset;

                }
                else if (target.x < AttachedExemon.transform.position.y)
                {

                    baseAngle -= distance * AimHeightOffset;

                }



                if ((baseAngle < MinAimAngle && baseAngle > (-180 + MinAimAngle)))
                {

                    if (target.x > AttachedExemon.transform.position.x)
                    {
                        baseAngle = MinAimAngle;
                    }
                    else
                    {
                        baseAngle = 180 - MinAimAngle;

                    }

                }
                else if (((baseAngle > MaxAimAngle && baseAngle < (180 - MaxAimAngle))))
                {



                    if (target.x > AttachedExemon.transform.position.x)
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


                projectile.GetComponent<Rigidbody2D>().velocity = (new Vector2(xcomponent, ycomponent));

            }
            if (shootData.projectileDirection == SpawnDirection.Up)
            {

                projectile.GetComponent<Rigidbody2D>().velocity = (transform.up * shootData.force);

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
                    if (projectile != null)
                    {
                        var projectileData = projectile.GetComponent<Projectile>();
                        if (projectileData.Index == forceData.ProjectileIndex)
                        {
                            if (Vector2.Distance(projectile.GetComponent<Projectile>().transform.position, AttachedExemon.transform.position) < 2){

                                Vector2 position = projectile.gameObject.transform.position;
                                var target = AttachedExemon.GetComponent<BattleEntity>().target;
                                Vector2 difference = target - position;
                                float sign = (AttachedExemon.GetComponent<BattleEntity>().target.y < position.y) ? -1.0f : 1.0f;
                                var baseAngle = Vector2.Angle(Vector2.right, difference) * sign;
                                //Debug.Log(baseAngle);
                                //baseAngle = baseAngle + (Mathf.Abs(AttachedExemon.transform.position.x - target.x) * AimHeightOffset);
                                var distance = Mathf.Abs(AttachedExemon.transform.position.x - target.x);
                                if (distance > MaxDistanceOffset)
                                {
                                    distance = MaxDistanceOffset;
                                }
                                if (target.x > AttachedExemon.transform.position.x)
                                {

                                    baseAngle += distance * AimHeightOffset;

                                }
                                else if (target.x < AttachedExemon.transform.position.y)
                                {

                                    baseAngle -= distance * AimHeightOffset;

                                }



                                if ((baseAngle < MinAimAngle && baseAngle > (-180 + MinAimAngle)))
                                {

                                    if (target.x > AttachedExemon.transform.position.x)
                                    {
                                        baseAngle = MinAimAngle;
                                    }
                                    else
                                    {
                                        baseAngle = -180 + MinAimAngle;

                                    }

                                }
                                else if (((baseAngle > MaxAimAngle && baseAngle < (180 - MaxAimAngle))))
                                {

                                    if (target.x > AttachedExemon.transform.position.x)
                                    {
                                        baseAngle = MaxAimAngle;
                                    }
                                    else
                                    {
                                        baseAngle = 180 - MaxAimAngle;
                                    }
                                }
                                baseAngle += (Random.Range(-Accuracy, Accuracy));
                                float xcomponent = Mathf.Cos(baseAngle * Mathf.PI / 180) * forceData.force;
                                float ycomponent = Mathf.Sin(baseAngle * Mathf.PI / 180) * forceData.force;

                                projectile.GetComponent<Rigidbody2D>().velocity = (new Vector2(xcomponent, ycomponent));
                            }

                        }
                        else if (forceData.ProjectileIndex == -1)
                        {
                            //Make EVERY projectile change direction;
                        }
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

public enum ForceDirection
{
    CustomAngle,
    ImpactPoint,
    ProjectileVelocity,
}