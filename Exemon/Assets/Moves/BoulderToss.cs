using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderToss : Move
{
    public bool HitBoulder;

    public GameObject lProjectile;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (HitBoulder)
        {
            ApplySecondForce();
            HitBoulder = false;
        }

        var battleExemon = AttachedExemon.GetComponent<BattleExemon>();
        if (battleExemon.nextState != State.Idle && canExitAttack)
        {
            battleExemon.EndAttack();
        }
    }

    public override void LaunchRangedAttack()
    {
        lProjectile = Instantiate(Projectile, ProjectileSpawn);
        lProjectile.GetComponent<Projectile>().SetValues(AttachedExemon, this, ProjectileForce, ProjectileAngle);
    }

    public void ApplySecondForce()
    {
        HitBoulder = false;
        var attachedExemon = AttachedExemon.GetComponent<BattleExemon>();
        if (lProjectile != null)
        {
            var angle = 0;
            if (attachedExemon.TurnedAround)
                angle = -45;
            else
                angle = 45;
            lProjectile.GetComponent<Projectile>().ArcFire(attachedExemon.enemyExemon.transform, angle);
        }
    }

}
