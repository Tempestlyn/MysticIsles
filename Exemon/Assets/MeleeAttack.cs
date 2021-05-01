using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : Move
{
    public List<HitBox> HitBoxes;
    

    private float MoveTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveTime += Time.deltaTime;
        HitDelay -= Time.deltaTime;
        CanMove = moveTime >= movementDelay;//TODO: CAN MOVE WILL BE BASED OFF OF List OF VECTOR2s that indicates the times the player can/can't move
        canExitAttack = moveTime >= lockedTime;

        foreach (HitBox hitBox in HitBoxes)
        {
            if ((MoveTime >= hitBox.StartTime) && (MoveTime <= hitBox.EndTime))
            {
                hitBox.IsActive = true;
                //hitBox.
            }
            else
            {
                hitBox.IsActive = false;
            }
        }


    }
}
