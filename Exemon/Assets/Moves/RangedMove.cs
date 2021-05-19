using System.Collections;
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
        StartPosition = AttachedExemon.transform.position;
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
            //projectile.Force = projectileShootData.force;
            projectile.ForceAngle = projectileShootData.forceAngle;
            projectile.LifeSpan = projectileShootData.LifeSpan;
            StartCoroutine(Shoot(projectileShootData));

        }
        foreach(AimLockedTime aimLockedTime in AimLockedTimes)
        {
            StartCoroutine(AttachedExemon.GetComponent<BattleExemon>().LockAim(aimLockedTime));
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
            Debug.Log("Test");
            AttachedExemon.GetComponent<BattleExemon>().EndAttack();
        }

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




    }





    IEnumerator Shoot(ProjectileShootData shootData)
    {
        //Debug.Log(shootData.projectile);
        if (Random.value < (shootData.ChanceToSpawn / 100)){
            
            yield return new WaitForSeconds(shootData.TimeToShoot);

            if (shootData.projectileDirection == SpawnDirection.Mouse)
            {
               

                Vector2 myPos = ProjectileSpawn.transform.position;
                var target = AttachedExemon.GetComponent<BattleExemon>().target;
                Vector2 difference = target - myPos;
                
                float sign = (AttachedExemon.GetComponent<BattleExemon>().target.y < myPos.y) ? -1.0f : 1.0f;
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
                        

                        Vector2 position = projectile.gameObject.transform.position;
                        var target = AttachedExemon.GetComponent<BattleExemon>().target;
                        Vector2 difference = target - position;
                        float sign = (AttachedExemon.GetComponent<BattleExemon>().target.y < position.y) ? -1.0f : 1.0f;
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
