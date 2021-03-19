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
                FaceForward();
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
            if (Input.GetKeyDown(KeyCode.Alpha1)) 
                Attack(Moves[0].GetComponent<Move>());
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.D))
                nextState = State.RunningForward;
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.A))
                nextState = State.RunningBackward;
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
    public void ApplyStun()
    {

    }
    void TakeDamage(int health)
    {
        
    }

    void WalkForward()
    {
        rigidbody.velocity = new Vector2(speed, rigidbody.velocity.y);
        animator.SetBool("IsMoving", true);
    }
    void RunForward()
    {
        rigidbody.velocity = new Vector2(speed * 1.5f, rigidbody.velocity.y);
        transform.rotation = new Quaternion(transform.rotation.x, 0, transform.rotation.z, transform.rotation.w);
        animator.SetBool("IsMoving", true);

    }
    void WalkBackward()
    {
        rigidbody.velocity = new Vector2(-speed, rigidbody.velocity.y);
        animator.SetBool("IsMoving", true);
    }
    void RunBackward()
    {
        rigidbody.velocity = new Vector2(-speed * 1.5f, rigidbody.velocity.y);
        transform.rotation = new Quaternion(transform.rotation.x, 180, transform.rotation.z, transform.rotation.w);
        animator.SetBool("IsMoving", true);
    }
    void Attack(Move move)
    {
        if (ActiveMove == null)
        {
            ActiveMove = move;
            animator.SetInteger("MoveID", move.MoveID);
            finishedAttack = false;
        }
    }


    void SetIdle()
    {
        animator.SetBool("IsMoving", false);
    }

    public void HitTest()
    {
        //Debug.Log("Hit!");
    }


    public void StartWalkingBackwards()
    {

    }

    public void EndAttack()
    {
        ActiveMove = null;
        animator.SetInteger("MoveID", 0);
        finishedAttack = false;
        
        
        
    }

    public void FaceForward()
    {

    }
    public void FaceBackward()
    {

    }

 

}



    public enum Stance
    {
        Attack,
        Defend,
        StayAway,
        
    }

public enum Command
{
    None,
    Charge,
    GetAway,


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








