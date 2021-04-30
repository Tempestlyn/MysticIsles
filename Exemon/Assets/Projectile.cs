using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float damage;
    public GameObject controllingExemon;
    public Move controllingMove;
    public bool DestroyOnHit;
    private float ForceAngle;
    private float Force;
    public float StunDuration;
    public AnimationCurve UpMovement;
    public bool InitialMovement;
    public float TimeElapsed;
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
        if (collider.gameObject.GetComponent<ExemonHitbox>() && collider.gameObject.GetComponent<ExemonHitbox>().battleExemon != controllingMove.AttachedExemon)
        {
            var exemon = collider.gameObject.GetComponent<ExemonHitbox>().battleExemon.gameObject.GetComponent<BattleExemon>();
            controllingMove.ResolveHit(exemon, StunDuration);

            if (DestroyOnHit)
            {
                Destroy(gameObject);
            }
            
        }
        else if (!collider.gameObject.GetComponent<BattleExemon>())
        {
            if (collider.gameObject.GetComponent<Projectile>())
            {
                if(collider.gameObject.GetComponent<Projectile>().controllingExemon != controllingExemon)
                {
                    //TODO: Move Collision
                }
            }
        }

        
    }

    public void ApplyForce(float force, float forceAngle)
    {
        //Force = force;
        //InitialMovement = true;
        //Debug.Log(Force);
        float xcomponent = Mathf.Cos(forceAngle * Mathf.PI / 180) * force;
        float ycomponent = Mathf.Sin(forceAngle * Mathf.PI / 180) * force;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(xcomponent, ycomponent));


    }

    private void FixedUpdate()
    {


        //if (InitialMovement)
        //{

        //TimeElapsed += Time.deltaTime;
        //GetComponent<Rigidbody2D>().AddForce(new Vector2(0, UpMovement.Evaluate(TimeElapsed) * Force));
        //}
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


}
