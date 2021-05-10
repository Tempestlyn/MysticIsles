﻿using System.Collections;
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
        Vector3 pos = transform.position;
        Vector3 dir = BattleScene.BattleCam.ScreenToWorldPoint(Input.mousePosition) - pos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;


        if (gameObject.GetComponentInParent<BattleExemon>().ActiveMove != null)

        {
            if (angle < gameObject.GetComponentInParent<BattleExemon>().ActiveMove.MinAimAngle)
            {
                angle = gameObject.GetComponentInParent<BattleExemon>().ActiveMove.MinAimAngle;
            }
            if (angle > gameObject.GetComponentInParent<BattleExemon>().ActiveMove.MaxAimAngle)
            {
                angle = gameObject.GetComponentInParent<BattleExemon>().ActiveMove.MaxAimAngle;
            }
        }

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);



        


    }
}
