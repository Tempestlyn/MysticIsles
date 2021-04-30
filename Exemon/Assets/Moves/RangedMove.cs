using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedMove : Move
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveTime += Time.deltaTime;
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

                    shotsLeft -= 1;
                    DelayTime = ProjectileDelay;
                }
                
            }
        }
        if(moveTime >= lockedTime && AttachedExemon.GetComponent<BattleExemon>().nextState != State.Idle)
        {
            
            canExitAttack = true;
            AttachedExemon.GetComponent<BattleExemon>().EndAttackAnimation();
        }
        
        var battleExemon = AttachedExemon.GetComponent<BattleExemon>();

        
        DelayTime -= Time.deltaTime;
    }
}
