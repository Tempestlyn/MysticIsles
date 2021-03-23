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
    public void SetValues(GameObject lControllingExemon, Move lControllingMove, float force, float forceAngle)
    {
        controllingExemon = lControllingExemon;

        float xcomponent = Mathf.Cos(forceAngle * Mathf.PI / 180) * force;
        float ycomponent = Mathf.Sin(forceAngle * Mathf.PI / 180) * force;
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(xcomponent, ycomponent));
    }
         

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<BattleExemon>() && collider.gameObject != controllingExemon)
        {
            var exemon = collider.gameObject.GetComponent<BattleExemon>();
            controllingMove.ResolveHit(exemon, StunDuration);
        }
    }


    }
