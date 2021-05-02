using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : Move
{
    public List<GameObject> HitBoxes;


    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject hitBox in HitBoxes)
        {
            hitBox.GetComponent<HitBox>().AttachedMove = this;
                

        }
    }

        // Update is called once per frame
        void Update()
        {

        foreach (Vector2 time in NoMovementTimes)
        {
            if (moveTime < time[0] || moveTime > time[1])
            {
                CanMove = true;
            }
            else
            {
                CanMove = false;
            }
        }
        moveTime += Time.deltaTime;
            HitDelay -= Time.deltaTime;

        if (moveTime >= MaxMoveTime || AttachedExemon.GetComponent<BattleExemon>().ActiveMove != this)
        {
            Destroy(gameObject);
        }

            foreach (Vector2 time in lockedTimes)
            {
                if (moveTime >= time[0] && moveTime <= time[1])
                {
                    canExitAttack = false;
                }
                else
                {
                    canExitAttack = true;
                }

            }

            foreach (GameObject hitBox in HitBoxes)
            {
            var hitBoxScript = hitBox.GetComponent<HitBox>();
                if ((moveTime >= hitBoxScript.StartTime) && (moveTime <= hitBoxScript.EndTime))
                {
                hitBoxScript.IsActive = true;

                }
                else
                {
                hitBoxScript.IsActive = false;
                }
            }


        }
    }

