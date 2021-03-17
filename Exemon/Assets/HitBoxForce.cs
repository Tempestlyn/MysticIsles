using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxForce : MonoBehaviour
{
    public float force;
    public float angle;
    public BoxCollider2D boxCollider;
    public bool isColliding;
    public GameObject ControllingMove;

    public void AddForceAtAngle(float force, float angle, Rigidbody2D exemon)
    {
        float xcomponent = Mathf.Cos(angle * Mathf.PI / 180) * force;
        float ycomponent = Mathf.Sin(angle * Mathf.PI / 180) * force;

        exemon.AddForce(new Vector2(xcomponent, ycomponent));
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        var move = ControllingMove.GetComponent<Move>();
        
        if (collider.gameObject.GetComponent<ExemonHitbox>() && collider.gameObject.GetComponent<ExemonHitbox>().battleExemon != ControllingMove.GetComponent<Move>().AttachedExemon)
        {
            var hitExemon = collider.gameObject.GetComponent<ExemonHitbox>().battleExemon.gameObject.GetComponent<BattleExemon>();
            move.ResolveHit(hitExemon);

            AddForceAtAngle(100, 70, hitExemon.gameObject.GetComponent<Rigidbody2D>());
 
            //gameObject.SetActive(false);

        }
        
    }


    public bool IsColliding()
    {
        return isColliding;
    }
}
