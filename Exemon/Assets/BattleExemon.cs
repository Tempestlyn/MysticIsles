using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleExemon : MonoBehaviour
{
    public bool PlayerControlled;

    public BaseExemon exemon;
    public Stance stance;
    public State state;
    public GameObject move1;
    private Rigidbody2D rigidbody;
    private Animator animator;
    public float speed;
    public Move ActiveMove;
    public List<int> LearnableMoveIds;
    public List<GameObject> Moves;
    public GameObject enemyExemon;

    public bool finishedAttack;
    // Start is called before the first frame update


    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        stance = exemon.defaultStance;
    }

        // Update is called once per frame
        void Update()
        {
            if (finishedAttack)
            {
                CancelAttack();
            }

        if (state != State.Falling)
        {
            if (stance == Stance.Attack)
            {
                if (ActiveMove != null)
                {
                    if (!ActiveMove.mustStandStill)
                    {
                        Move();
                    }
                    else
                    {
                        
                    }
                }
                else
                {
                    Move();
                    SetWalking();
                }
                
                    

            }
            if (stance == Stance.StayAway)

            {
                Move();
                SetWalking();

            }
            if (stance == Stance.Defend)
            {
                SetIdle();
            }
        }
            

        if (PlayerControlled)
        {
            if (Input.GetKeyDown(KeyCode.D))
                stance = Stance.Attack;
            if (Input.GetKeyDown(KeyCode.S))
                stance = Stance.Defend;
            if (Input.GetKeyDown(KeyCode.A))
                stance = Stance.StayAway;
            if (Input.GetKeyDown(KeyCode.Alpha1)) 
                Attack(Moves[0].GetComponent<Move>());
            //if (Input.GetKeyDown(KeyCode.Q))
                //CancelAttack();
        }

        }
        void Move()
        {


        if (stance == Stance.Attack)
        {
            float dist = transform.position.x - enemyExemon.transform.position.x;



            if (exemon.Reach < Mathf.Abs(dist))
            {
                float x = 0;
                if (enemyExemon.transform.position.x > transform.position.x)
                {
                    x = 1;
                }
                else if (enemyExemon.transform.position.x < transform.position.x)
                {
                    x = -1;
                }


                float moveBy = x * speed;
                rigidbody.velocity = new Vector2(moveBy, rigidbody.velocity.y);
            }
            else
            {
                rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
                Debug.Log("test");

            }
        }
        if (stance == Stance.StayAway)
        {

            float dist = transform.position.x - enemyExemon.transform.position.x;



            if (exemon.Reach < Mathf.Abs(dist))
            {
                float x = 0;
                if (enemyExemon.transform.position.x > transform.position.x)
                {
                    x = -1;
                }
                else if (enemyExemon.transform.position.x < transform.position.x)
                {
                    x = 1;
                }


                float moveBy = x * speed;
                rigidbody.velocity = new Vector2(moveBy, rigidbody.velocity.y);
            }
            else
            {
                rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
                Debug.Log("test");

            }


        }
         
            


        }

    void Attack(Move move)
    {
        if (move.mustStandStill)
        {
            rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
        }
        ActiveMove = move;
        animator.SetInteger("MoveID", move.MoveID);

    }

    void SetWalking()
    {
        state = State.Walking;
        animator.SetBool("IsMoving", true);
    }

    void SetIdle()
    {
        state = State.None;
        animator.SetBool("IsMoving", false);
    }


    public void StartWalkingBackwards()
    {

    }

    public void CancelAttack()
    {
        ActiveMove = null;
        animator.SetInteger("MoveID", 0);
        finishedAttack = false;
        
        
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

    None, 
    Attacking,
    Walking,
    Flying,
    Staggered,
    Falling,
    

}

public struct MoveData
{
    public float hitTime;
}








