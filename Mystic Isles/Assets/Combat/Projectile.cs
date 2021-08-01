using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float damage;
    public GameObject controllingExemon;
    public Move controllingMove;
    public bool DestroyOnHit;
    public float ForceAngle;
    private Vector2 vectorAngle;
    public float Force;
    public float StunDuration;
    public float Health;
    public float LifeSpan;
    public int Index;
    public bool HitBox;
    public float DamageDelay;
    public Vector3 StartPosition;
    public ForceDirection forceDirection;
    
    private List<GameObject> HitObjects = new List<GameObject>();



    public void SetValues(GameObject lControllingExemon, Move lControllingMove, float force, float forceAngle)
    {
        controllingExemon = lControllingExemon;
        Force = force;
        float xcomponent = Mathf.Cos(forceAngle * Mathf.PI / 180) * force;
        float ycomponent = Mathf.Sin(forceAngle * Mathf.PI / 180) * force;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(xcomponent, ycomponent));
    }
         

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<BattleEntity>() && collider.gameObject != controllingMove.AttachedExemon /* && !HitObjects.Contains(collider.gameObject)*/)
        {
            

            if (forceDirection == ForceDirection.ImpactPoint)
            {
                var dir = collider.transform.position - transform.position;
                // normalize force vector to get direction only and trim magnitude
                dir = -dir.normalized;
                vectorAngle = dir;
            }

            StartCoroutine(ApplyDamage(DamageDelay, collider.gameObject, DamageType.BattleEntity));
            //var exemon = collider.gameObject.GetComponent<ExemonHitbox>().battleExemon.gameObject.GetComponent<BattleExemon>();
            //controllingMove.ResolveHit(exemon, damage, StunDuration, Force, ForceAngle);

            if (DestroyOnHit)
            {
                Destroy(gameObject);
            }
            
        }

            if (collider.gameObject.GetComponent<Projectile>())
            {
                if(collider.gameObject.GetComponent<Projectile>().controllingExemon != controllingExemon)
                {
                    Debug.Log("TEst");
                    StartCoroutine(ApplyDamage(DamageDelay, collider.gameObject, DamageType.Projectile));
                }
                
                
            }
            if (collider.gameObject.GetComponent<HitBox>())
            {

            }
        

        
    }

    public void ApplyForce(Vector2 target, Vector2 position, float force)
    {
        

        Vector2 difference = target - position;
        float sign = (target.y < position.y) ? -1.0f : 1.0f;
        var baseAngle = Vector2.Angle(Vector2.right, difference) * sign;
        float xcomponent = Mathf.Cos(baseAngle * Mathf.PI / 180) * force;
        float ycomponent = Mathf.Sin(baseAngle * Mathf.PI / 180) * force;

        //float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;

        GetComponent<Rigidbody2D>().AddForce(new Vector2(xcomponent, ycomponent));
    }



    public void ArcFire(Transform target, float initialAngle)
    {
        var rigid = GetComponent<Rigidbody2D>();

        Vector3 p = target.position;

        float gravity = Physics.gravity.magnitude;
        // Selected angle in radians
        float angle = initialAngle * Mathf.Deg2Rad;

        // Positions of this object and the target on the same plane
        Vector3 planarTarget = new Vector3(p.x, 0, p.z);
        Vector3 planarPostion = new Vector3(transform.position.x, 0, transform.position.z);

        // Planar distance between objects
        float distance = Vector3.Distance(planarTarget, planarPostion);
        // Distance along the y axis between objects
        float yOffset = transform.position.y - p.y;

        float initialVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));

        Vector3 velocity = new Vector3(0, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));

        // Rotate our velocity to match the direction between the two objects
        float angleBetweenObjects = Vector3.Angle(Vector3.forward, planarTarget - planarPostion) * (p.x > transform.position.x ? 1 : -1);
        Vector2 finalVelocity = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;

        // Fire!
        rigid.velocity = finalVelocity;
    }

    public virtual void OnEndAttackEvent()
    {

    }

    public IEnumerator ApplyDamage(float delayTime, GameObject hitObject, DamageType damageType)
    {

        
        //Debug.Log("test");
        if (damageType == DamageType.BattleEntity && !HitObjects.Contains(hitObject))
        {
            controllingMove.ResolveHitExemon(hitObject.gameObject, gameObject, forceDirection, damage, StunDuration, vectorAngle.x, vectorAngle.y, Force, ForceAngle);
            HitObjects.Add(hitObject);
        }
        if (damageType == DamageType.Projectile)
        {
            controllingMove.ResolveHitProjectile(hitObject.gameObject.GetComponent<Projectile>(), gameObject, forceDirection, damage, vectorAngle.x, vectorAngle.y);
        }
        yield return new WaitForSeconds(delayTime);

        //Debug.Log("Can be damaged");
        HitObjects.Remove(hitObject);
    }


    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
