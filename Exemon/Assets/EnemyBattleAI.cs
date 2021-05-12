
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattleAI : MonoBehaviour
{

    private BattleExemon battleExemon;
    private float nextMoveDelay;
    public bool AttacksUnprovoked;
    public float HostileRange;
    public Move nextMove;

    // Start is called before the first frame update
    void Start()
    {
        battleExemon = gameObject.GetComponent<BattleExemon>();
    }

    // Update is called once per frame
    void Update()
    {
        nextMoveDelay -= Time.deltaTime;

        if (nextMove == null)
        {
            nextMove = battleExemon.Moves[UnityEngine.Random.Range(0, battleExemon.Moves.Count)];
        }

        var reach = nextMove.AIRange;

        if (battleExemon.PlayerControlled != true)
        {


            battleExemon.nextState = State.Idle;
            var dist = Vector3.Distance(gameObject.transform.position, battleExemon.enemyExemon.gameObject.transform.position);
            if (Math.Abs(dist) > reach)

            {
                if (battleExemon.ActiveMove == null)
                {
                    
                    if (battleExemon.enemyExemon.transform.position.x < gameObject.transform.position.x)
                    {
                        battleExemon.nextState = State.RunningBackward;
                    }
                    else if (battleExemon.enemyExemon.transform.position.x > gameObject.transform.position.x)
                    {
                        battleExemon.nextState = State.RunningForward;
                    }
                }
                else
                {
                    
                }
            }
            else if (nextMoveDelay <= 0)
            {

                if (battleExemon.ActiveMove == null)
                {
                    battleExemon.Attack(battleExemon.Moves[UnityEngine.Random.Range(0, battleExemon.Moves.Count - 1)]);
                    Debug.Log("Test");
                    //WaitForNextAttack(UnityEngine.Random.Range(0, 3));
                }

                
                //battleExemon.nextState = State.Idle;

            }
        }

    }

    void WaitForNextAttack(float time)
    {
        nextMoveDelay = time;
    }



}
