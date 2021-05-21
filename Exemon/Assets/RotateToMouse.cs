using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToMouse : MonoBehaviour
{

    public GameObject SpawnPoint;
    public float SpawnRadius;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!gameObject.GetComponentInParent<BattleExemon>().AimLocked)
        {


            Vector2 pos = transform.position;
            Vector3 dir = gameObject.GetComponentInParent<BattleExemon>().target - pos;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;


            if (gameObject.GetComponentInParent<BattleExemon>().ActiveMove != null)
            {
                var activeMove = gameObject.GetComponentInParent<BattleExemon>().ActiveMove;


                if ((angle < activeMove.MinAimAngle && activeMove.MinAimAngle > (-180 + activeMove.MinAimAngle)))
                {
                    if (!gameObject.GetComponentInParent<BattleExemon>().TurnedAround)
                    {
                        angle = activeMove.MinAimAngle;
                    }
                    else
                    {

                        angle = -180 + activeMove.MinAimAngle;
                    }
                }
                else if (((angle > activeMove.MaxAimAngle && angle < (180 - activeMove.MaxAimAngle))))
                {
                    if (gameObject.GetComponentInParent<BattleExemon>().TurnedAround)
                    {
                        
                        angle = 180 - activeMove.MaxAimAngle;
                    }
                    else
                    {
                        angle = activeMove.MaxAimAngle;
                    }
                }
            }

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);



        }
        


    }
}
