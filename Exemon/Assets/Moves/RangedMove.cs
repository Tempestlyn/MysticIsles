using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedMove : Move
{
    public float ProjectileAngle;
    public int ProjectileAmount;
    public GameObject Projectile;
    public float ProjectileForce;
    public Transform ProjectileSpawn;
    public float ProjectileDelay;
    
    // Start is called before the first frame update
    void Start()
    {
        ProjectileSpawn = AttachedExemon.GetComponent<BattleExemon>().MoveSpawn.transform;
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

        if (shotsLeft > 0 && AttachedExemon && AttachedExemon.GetComponent<BattleExemon>().ActiveMove == this)
        {
            if (startDelay <= moveTime) {

                if (DelayTime <= 0)
                {
                    Vector3 worldPoint = BattleScene.BattleCam.ScreenToWorldPoint(Input.mousePosition);
                    var accuracyMod = Random.Range(-Accuracy, Accuracy);

                    Vector2 target = BattleScene.BattleCam.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
                    Vector2 myPos = ProjectileSpawn.transform.position;

                    Vector2 difference = target - myPos;
                    float sign = (target.y < myPos.y) ? -1.0f : 1.0f;
                    var baseAngle = Vector2.Angle(Vector2.right, difference) * sign;
                    baseAngle += (Random.Range(-Accuracy, Accuracy));
                    float xcomponent = Mathf.Cos(baseAngle * Mathf.PI / 180) * ProjectileForce;
                    float ycomponent = Mathf.Sin(baseAngle * Mathf.PI / 180) * ProjectileForce;

                    //float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
                    var projectile = Instantiate(Projectile, ProjectileSpawn.transform);
                    projectile.gameObject.transform.parent = null;
                    
                    projectile.GetComponent<Rigidbody2D>().AddForce(new Vector2(xcomponent, ycomponent));
                    projectile.GetComponent<Projectile>().controllingMove = this;
                    projectile.GetComponent<Projectile>().controllingExemon = AttachedExemon;
                    projectile.GetComponent<Projectile>().damage = Damage;
                    shotsLeft -= 1;
                    DelayTime = ProjectileDelay;
                }
                
            }
        }

        var battleExemon = AttachedExemon.GetComponent<BattleExemon>();

        
        DelayTime -= Time.deltaTime;
    }

    public override void InitiateAttack()
    {
        shotsLeft = ProjectileAmount;
    }
}
