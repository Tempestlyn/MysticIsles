using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToMouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!gameObject.GetComponentInParent<BattleExemon>().AimLocked)
        {


            Vector3 pos = transform.position;
            Vector3 dir = BattleScene.BattleCam.ScreenToWorldPoint(Input.mousePosition) - pos;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;


            if (gameObject.GetComponentInParent<BattleExemon>().ActiveMove != null)
            {
                var activeMove = gameObject.GetComponentInParent<BattleExemon>().ActiveMove;


                if ((angle < activeMove.MinAimAngle && activeMove.MinAimAngle > (-180 + activeMove.MinAimAngle)))
                {
                    if (BattleScene.BattleCam.ScreenToWorldPoint(Input.mousePosition).x > activeMove.AttachedExemon.transform.position.x)
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
                    if (BattleScene.BattleCam.ScreenToWorldPoint(Input.mousePosition).x > activeMove.AttachedExemon.transform.position.x)
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
