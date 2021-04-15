using System.Collections;
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
    public List<GameObject> Moves;
    public GameObject enemyExemon;
    public bool TurnedAround;
    public bool WithinReach;



    public float TimeStunned;
    public float AttackLockTime;
    
    public bool finishedAttack;

    public Animator hairAnimator;
    public Animator RobeAnimator;
    // Start is called before the first frame update


    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //stance = exemon.defaultStance;
        foreach (GameObject move in Moves)
        {
            move.GetComponent<Move>().AttachedExemon = gameObject;
        }
    }

    void SetStats()
    {
        //health = exemon.baseHP;
    }


        // Update is called once per frame
        void Update()
        {

        if (finishedAttack)
        {
            EndAttack();
        }
        var canMove = true;

        if (ActiveMove != null)
        {
            canMove = !ActiveMove.mustStandStill;
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
            if (canMove)
                WalkForward();

        }
        if (currentState == State.RunningForward)
        {
            if (canMove)
            {
                RunForward();
            }
        }
        if (currentState == State.WalkingBackward)
        {
            if (canMove)
            {
                WalkBackward();
            }
        }
        if (currentState == State.RunningBackward)
        {
            if (canMove)
            {
                RunBackward();
            }
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
                Attack(Moves[0].GetComponent<Move>());
            if (Input.GetKeyDown(KeyCode.Alpha2))
                Attack(Moves[1].GetComponent<Move>());
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
        TurnedAround = true;
    }
    public void Attack(Move move)
    {
        if (ActiveMove == null && TimeStunned <= 0)
        {
            
            ActiveMove = move;
            animator.SetInteger("MoveID", move.MoveID);
            ActiveMove.LaunchRangedAttack();
            finishedAttack = false;
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
        finishedAttack = false;

    }

    public void LaunchProjectile(int MoveID)
    {
        foreach (GameObject move in Moves)
        {
            if (MoveID == move.GetComponent<Move>().MoveID)
            {
                var moveScript = move.GetComponent<Move>();
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








