﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleExemon : MonoBehaviour
{
    public bool PlayerControlled;

    //public BaseExemon exemon;
    public Stance stance;
    public State nextState;
    public State currentState;
    public float reach;
    public GameObject move1;
    private Rigidbody2D rigidbody;
    private Animator animator;
    public float speed;
    public float health;
    public float jumpHeight;
    public Move ActiveMove;
    public List<Move> Moves;
    public GameObject enemyExemon;
    public bool TurnedAround;
    public bool WithinReach;
    public GameObject MoveSpawn;
    public bool IsTryingToExitAttack;

    public int SelectedAttackIndex;
    public bool isAttacking;

    public float TimeStunned;
    public float AttackLockTime;
    

    public Animator hairAnimator;
    public Animator RobeAnimator;
    // Start is called before the first frame update


    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //stance = exemon.defaultStance;
        
    }

            


    void SetStats()
    {
        //health = exemon.baseHP;
    }


        // Update is called once per frame
        void Update()
        {

        var CanMove = true;

        if (ActiveMove != null)
        {
            if (!ActiveMove.CanMove)
            {
                if (ActiveMove.canExitAttack && nextState != State.Idle)
                {
                    IsTryingToExitAttack = true;
                }
            }
            CanMove = ActiveMove.CanMove;

            if (IsTryingToExitAttack && ActiveMove.canExitAttack)
            {
                EndAttack();
            }
        }
        
        if (nextState != currentState)
        {
            if(currentState != State.Stunned)
            {
                currentState = nextState;

            }
            else
            {
                ResolveStun();
            }

        }

        if (currentState == State.Idle)
        {
            SetIdle();
        }
        if (currentState == State.WalkingForward)
        {
            if (CanMove)
                WalkForward();

        }
        if (currentState == State.RunningForward)
        {
            if (CanMove)
            {
                RunForward();
            }
        }
        if (currentState == State.WalkingBackward)
        {
            if (CanMove)
            {
                WalkBackward();
            }
        }
        if (currentState == State.RunningBackward)
        {
            if (CanMove)
            {
                RunBackward();
            }
        }

        IsTryingToExitAttack = false;



        if (!Input.GetMouseButton(0))
        {
            IsTryingToExitAttack = true;
        }



        




        if (PlayerControlled)
        {
            if (Input.GetKeyDown(KeyCode.D))
                nextState = State.WalkingForward;
            if (Input.GetKeyDown(KeyCode.S))
                nextState = State.Idle;
            if (Input.GetKeyDown(KeyCode.A))
                nextState = State.WalkingBackward;

            if (Input.GetKeyUp(KeyCode.D))
            {
                StopWalking();
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
                SelectedAttackIndex = 0;
            if (Input.GetKeyDown(KeyCode.Alpha2))
                SelectedAttackIndex = 0;
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.D))
                nextState = State.RunningForward;
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.A))
                nextState = State.RunningBackward;
            
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                if(currentState == State.RunningBackward)
                {
                    nextState = State.WalkingBackward;
                }
                if (currentState == State.RunningForward)
                {
                    nextState = State.WalkingForward;
                }
            }
            if (!Input.anyKey)
            {
                nextState = State.Idle;
            }

            if (Input.GetMouseButtonDown(0))
            {
                Attack(Moves[SelectedAttackIndex]);
            }


            //if (Input.GetKeyDown(KeyCode.Q))
                //CancelAttack();

            
        }

        }
    public void ResolveStun()
    {
        TimeStunned -= Time.deltaTime;
        if(TimeStunned <= 0)
        {
            currentState = State.Idle;
        }
    }
    public void ApplyStun(float duration)
    {
        EndAttack();
        TimeStunned = duration;
        currentState = State.Stunned;
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    void WalkForward()
    {
        rigidbody.velocity = new Vector2(speed, rigidbody.velocity.y);
        animator.SetInteger("IsMoving", 1);
        SetChildAnimations("IsMoving", 1);

    }
    void RunForward()
    {
        rigidbody.velocity = new Vector2(speed * 1.5f, rigidbody.velocity.y);
        transform.rotation = new Quaternion(transform.rotation.x, 0, transform.rotation.z, transform.rotation.w);
        animator.SetInteger("IsMoving", 1);
        SetChildAnimations("IsMoving", 1);
        if (!PlayerControlled)
            TurnedAround = false;

    }
    void WalkBackward()
    {
        rigidbody.velocity = new Vector2(-speed, rigidbody.velocity.y);
        animator.SetInteger("IsMoving", 1);
        SetChildAnimations("IsMoving", 1);
    }
    void RunBackward()
    {
        rigidbody.velocity = new Vector2(-speed * 1.5f, rigidbody.velocity.y);
        transform.rotation = new Quaternion(transform.rotation.x, 180, transform.rotation.z, transform.rotation.w);
        animator.SetInteger("IsMoving", 1);
        SetChildAnimations("IsMoving", 1);
        if (!PlayerControlled)
            TurnedAround = true;
    }
    public void Attack(Move selectedMove)
    {
        
        if ((ActiveMove == null || (ActiveMove.lockedTime <= ActiveMove.moveTime)) && TimeStunned <= 0)
        {
            var move = Instantiate(selectedMove, gameObject.transform);
            move.AttachedExemon = gameObject;
            move.moveTime = 0;
            ActiveMove = move;
            animator.SetInteger("MoveID", move.MoveID);
            ActiveMove.InitiateAttack();
        }
        
    }


    void SetIdle()
    {
        animator.SetInteger("IsMoving", 0);
        SetChildAnimations("IsMoving", 0);
    }


    public void EndAttack()
    {
        ActiveMove = null;
        animator.SetInteger("MoveID", 0);

    }
    public void EndAttackAnimation()
    {
        animator.SetInteger("MoveID", 0);
    }
        

    public void LaunchProjectile(int MoveID)
    {
        foreach (Move move in Moves)
        {
            if (MoveID == move.MoveID)
            {
                var moveScript = move;
                moveScript.LaunchRangedAttack();
            }
        }
        
    }


    public void StopWalking()
    {
        rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
    }

    public void SetChildAnimations(string id, int value)
    {
        
        foreach(Transform child in gameObject.transform)
        {
            if (child.tag == "Robe" && child.gameObject.activeSelf)
            {
                child.gameObject.GetComponent<Animator>().SetInteger(id, value);
            }
        }
    }

    public void SetVelocity()
    {

    }
    public void AddToVelocity(Vector2 force)
    {

    }

}



    public enum Stance
    {
        Attack,
        Defend,
        StayAway,
        
    }



public enum State
{

    Idle, 
    Attacking,
    WalkingForward,
    WalkingBackward,
    RunningForward,
    RunningBackward,
    Flying,
    Stunned,
    Falling,
    

}

public struct MoveData
{
    public float hitTime;
}








