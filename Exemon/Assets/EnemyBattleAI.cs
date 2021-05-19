
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
    private bool WasAttackingLastFrame;
    public Vector2 Target = new Vector2(0, 0);
    // Start is called before the first frame update
    void Start()
    {
        battleExemon = gameObject.GetComponent<BattleExemon>();
    }

    // Update is called once per frame
    void Update()
    {
        nextMoveDelay -= Time.deltaTime;

        

        if (battleExemon.ActiveMove == null && battleExemon.animator.GetInteger("MoveID") == 0 && WasAttackingLastFrame) //TODO: This is probably the most jankiest, cringiest, and most downright braindead solution I can
        {                                                                                                               //come up with to keep the animations from gettings stuck... but It'll work for now. I really hope nobody ends up seeing this code.
            WasAttackingLastFrame = false;
            return;
        }

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
                
                if (battleExemon.ActiveMove == null && !WasAttackingLastFrame)
                {
                    if (battleExemon.currentState != State.Idle)
                    {
                        battleExemon.nextState = State.Idle;
                    }
                    else
                    {


                        battleExemon.EndAttack();
                        battleExemon.Attack(battleExemon.Moves[UnityEngine.Random.Range(0, battleExemon.Moves.Count - 1)]);
                        //Debug.Log("Test");
                        WasAttackingLastFrame = true;
                        WaitForNextAttack(UnityEngine.Random.Range(0, 3));
                    }
                } 

                
                //battleExemon.nextState = State.Idle;

            }
        }

        //target = BattleScene.BattleCam.ScreenToWorldPoint(new Vector2(AttachedExemon.GetComponent<BattleExemon>().enemyExemon.transform.position.x, AttachedExemon.GetComponent<BattleExemon>().enemyExemon.transform.position.y));

    }

    void WaitForNextAttack(float time)
    {
        nextMoveDelay = time;
    }
    


}
