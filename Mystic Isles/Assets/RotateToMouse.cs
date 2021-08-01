using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToMouse : MonoBehaviour
{

    public GameObject SpawnPoint;
    public float SpawnRadius;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        var parentEntity = gameObject.GetComponentInParent<BattleEntity>();
        if (!parentEntity.AimLocked)
        {


            Vector2 pos = transform.position;
            Vector3 dir = parentEntity.target - pos;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;


            if (gameObject.GetComponentInParent<BattleEntity>().ActiveMove != null)
            {
                var activeMove = parentEntity.ActiveMove;


                if ((angle < activeMove.MinAimAngle && activeMove.MinAimAngle > (-180 + activeMove.MinAimAngle)))
                {
                    if (!parentEntity.TurnedAround)
                    {
                        angle = activeMove.MinAimAngle;
                    }
                    else
                    {
                        //angle = 180 + activeMove.MinAimAngle;
                        angle = 180 - activeMove.MinAimAngle;
                    }
                }
                else if (((angle > activeMove.MaxAimAngle && angle < (180 - activeMove.MaxAimAngle))))
                {
                    if (!parentEntity.TurnedAround)
                    {
                        angle = activeMove.MaxAimAngle;
                        
                    }
                    
                    else
                    {

                        angle = 180 - activeMove.MaxAimAngle;
                    }
                }
            }

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);



        }
        


    }
}
