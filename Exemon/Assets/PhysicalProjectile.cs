 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalProjectile : Projectile
{
    private Rigidbody2D rigidbody;
    public float DamageSpeed;
    public float MinimumCollideSpeed;
    private void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }
    void OnCollisionEnter2D(Collision2D collider)
    {

        Debug.Log("Hit");
        if (collider.gameObject.GetComponent<ExemonHitbox>() && collider.gameObject.GetComponent<ExemonHitbox>().battleExemon != controllingMove.AttachedExemon && gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > DamageSpeed)
        {
            Debug.Log("Hit2");
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
                    Debug.Log("TEst");
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

            gameObject.layer = 11;
        }
        else
        {
            gameObject.layer = 8;
        }
    }
}
