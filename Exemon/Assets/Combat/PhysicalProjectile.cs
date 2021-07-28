 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalProjectile : Projectile
{
    private Rigidbody2D rigidbody;
    public float DamageSpeed;
    public float MinimumCollideSpeed;
    public bool FaceVelocity;

    private float TimeLowerThanCollideSpeed;
    private void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }
    void OnCollisionEnter2D(Collision2D collider)
    {


        if (collider.gameObject.GetComponent<BattleExemon>() && collider.gameObject != controllingMove.AttachedExemon && collider.relativeVelocity.magnitude > DamageSpeed)
        {

            StartCoroutine(ApplyDamage(DamageDelay, collider.gameObject, DamageType.Exemon));
            //var exemon = collider.gameObject.GetComponent<ExemonHitbox>().battleExemon.gameObject.GetComponent<BattleExemon>();
            //controllingMove.ResolveHit(exemon, damage, StunDuration, Force, ForceAngle);

            if (DestroyOnHit)
            {
                //Destroy(gameObject);
            }

        }
        else if (!collider.gameObject.GetComponent<BattleExemon>())
        {
            if (collider.gameObject.GetComponent<Projectile>())
            {
                if (collider.gameObject.GetComponent<Projectile>().controllingExemon != controllingExemon)
                {
                    StartCoroutine(ApplyDamage(DamageDelay, collider.gameObject, DamageType.Projectile));
                }


            }
            if (collider.gameObject.GetComponent<HitBox>())
            {

            }
        }



    }
    private void FixedUpdate()
    {



        if (rigidbody.velocity.magnitude < MinimumCollideSpeed)
        {
            TimeLowerThanCollideSpeed += Time.deltaTime;

            if (TimeLowerThanCollideSpeed > 1)
            {
                gameObject.layer = 11;
            }
        }
        else
        {
            TimeLowerThanCollideSpeed = 0;
            gameObject.layer = 8;
        }
    }




}
