using System.Collections.Generic;
using UnityEngine;

public class HitBoxForce : MonoBehaviour
{
    public List<GameObject> hitObjects = new List<GameObject>();
    public float force;
    public float angle;
    public BoxCollider2D boxCollider;
    public bool isColliding;
    public bool continueous;
    public GameObject ControllingMove;
 
    public void AddForceAtAngle(float force, float angle, Rigidbody2D exemon)
    {
        float xcomponent = Mathf.Cos(angle * Mathf.PI / 180) * force;
        float ycomponent = Mathf.Sin(angle * Mathf.PI / 180) * force;

        exemon.AddForce(new Vector2(xcomponent, ycomponent));
    }

    public void OnDisable()
    {
        hitObjects.Clear();
    }

    void OnTriggerStay2D(Collider2D collider)
    {

        var move = ControllingMove.GetComponent<Move>();
        if (collider.gameObject.GetComponent<ExemonHitbox>() && collider.gameObject.GetComponent<ExemonHitbox>().battleExemon != ControllingMove.GetComponent<Move>().AttachedExemon && !hitObjects.Contains(collider.gameObject))
        {
            if (!continueous)
            {
                hitObjects.Add(collider.gameObject);

            }
                var hitExemon = collider.gameObject.GetComponent<ExemonHitbox>().battleExemon.gameObject.GetComponent<BattleExemon>();

            
            
            move.ResolveHit(hitExemon);

            AddForceAtAngle(force, angle, hitExemon.gameObject.GetComponent<Rigidbody2D>());
            

            
            

        }
        
    }

    public void StopAttack()
    {
        gameObject.SetActive(true);
    }


    public bool IsColliding()
    {
        return isColliding;
    }
}
