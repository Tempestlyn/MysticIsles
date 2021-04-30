
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

    // Start is called before the first frame update
    void Start()
    {
        battleExemon = gameObject.GetComponent<BattleExemon>();
    }

    // Update is called once per frame
    void Update()
    {
        nextMoveDelay -= Time.deltaTime;


        if (battleExemon.PlayerControlled != true)
        {
            var dist = Vector3.Distance(gameObject.transform.position, battleExemon.enemyExemon.gameObject.transform.position);
            if (Math.Abs(dist) > battleExemon.reach)
            {

                if (battleExemon.enemyExemon.transform.position.x < gameObject.transform.position.x)
                {
                    battleExemon.nextState = State.RunningBackward;
                }
                if (battleExemon.enemyExemon.transform.position.x > gameObject.transform.position.x)
                {
                    battleExemon.nextState = State.RunningForward;
                }
            }
            else if (nextMoveDelay <= 0)
            {
                battleExemon.Attack(battleExemon.Moves[UnityEngine.Random.Range(0, battleExemon.Moves.Count - 1)]);
                WaitForNextAttack(UnityEngine.Random.Range(0, 3));
                //battleExemon.nextState = State.Idle;

            }
        }

    }

    void WaitForNextAttack(float time)
    {
        nextMoveDelay = time;
    }



}
