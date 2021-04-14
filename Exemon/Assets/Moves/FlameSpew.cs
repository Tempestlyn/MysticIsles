using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameSpew : Move
{
    // Start is called before the first frame update
    public override void LaunchRangedAttack()
    {
        for (int i = 0; i < ProjectileAmount; i++)
        {
            GameObject projectile = Instantiate(Projectile, ProjectileSpawn);
             projectile.GetComponent<Projectile>().ApplyForce(this.ProjectileForce, this.ProjectileAngle);
        }
    }
}
