using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleExemon : MonoBehaviour
{
    public bool PlayerControlled;

    //public BaseExemon exemon;
    


    public float reach;
    public Animator animator;
    public float health;
    public float speed;
    public float jumpForce;
    public List<Move> Moves;
    public GameObject MoveSpawn;
    public GameObject MoveRotation;
    public Vector2 target;
    public float AirGravity;
    
    public Animator hairAnimator;
    public Animator RobeAnimator;



    public BattleData BattleSystem;
    public GameObject enemyExemon;
    public GameObject TargetTest;
    

    [SerializeField]
    public LayerMask platformMask;

    private bool AIControlled;
    private Rigidbody2D rigidbody;

    [System.NonSerialized]
    public State nextState;
    [System.NonSerialized]
    public State currentState;
    [System.NonSerialized]
    public bool isAttacking;
    [System.NonSerialized]
    public float AttackLockTime;
    [System.NonSerialized]
    public float TimeStunned;
    [System.NonSerialized]
    public int SelectedAttackIndex;
    [System.NonSerialized]
    private bool IsTryingToExitAttack;
    [System.NonSerialized]
    private bool WithinReach;
    [System.NonSerialized]
    public Move ActiveMove;
    [System.NonSerialized]
    public bool TurnedAround;
    [System.NonSerialized]
    public bool AimLocked;
    [System.NonSerialized]
    private Vector2 AimLockedPosition = new Vector2(0, 0);

    public bool IsGrounded()
    {
        var collider2d = GetComponent<Collider2D>();
        RaycastHit2D raycastHit = Physics2D.BoxCast(collider2d.bounds.center, collider2d.bounds.size, 0f, Vector2.down, 0.5f, platformMask);

        return raycastHit.collider != null;
    }
    void Start()
    {
        AIControlled = gameObject.GetComponent<EnemyBattleAI>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        
    }

            


    void SetStats()
    {
        //health = exemon.baseHP;
    }

    private void FixedUpdate()
    {
        if(!PlayerControlled)
            

        PhysicsUpdate();
    }
    // Update is called once per frame
    void Update()
        {

        if (AimLocked)
        {
            target = AimLockedPosition;

        }
        else if (AIControlled)
        {
            target = new Vector2(enemyExemon.transform.position.x, enemyExemon.transform.position.y);
        }
        else
        {
            target = BattleSystem.BattleCam.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        }

        if (target.x > transform.position.x)
        {
            if (ActiveMove == null || ActiveMove.CanTurn)
            {
                TurnedAround = false;
                transform.rotation = new Quaternion(transform.rotation.x, 0, transform.rotation.z, transform.rotation.w);
            }
            else if(TurnedAround == true)
            {
                if (ActiveMove.ProjectileSpawn == null)
                {
                    target = new Vector2(ActiveMove.AttachedExemon.transform.position.x, target.y);
                }
                else
                {
                    target = new Vector2(ActiveMove.ProjectileSpawn.transform.position.x, target.y);
                }
            }
        }
        else if (target.x < transform.position.x)
        {
            if (ActiveMove == null || ActiveMove.CanTurn)
            {
                TurnedAround = true;
                transform.rotation = new Quaternion(transform.rotation.x, 180, transform.rotation.z, transform.rotation.w);
            }
            else if (TurnedAround == false)
            {
                if (ActiveMove.ProjectileSpawn == null)
                {
                    target = new Vector2(ActiveMove.AttachedExemon.transform.position.x, target.y);
                }
                else
                {
                    target = new Vector2(ActiveMove.ProjectileSpawn.transform.position.x, target.y);
                }
            }
        }

        if (TargetTest != null)
        {
            TargetTest.transform.position = target;
        }

        var CanMove = true;
        if (ActiveMove == null)
        {
            EndAttack();
        }
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
                WalkForward();
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



        if (!Input.GetMouseButton(0) && PlayerControlled)
        {
            IsTryingToExitAttack = true;
        }



        




        if (PlayerControlled)
        {
            if (Input.GetKey(KeyCode.D))
                nextState = State.WalkingForward;
            if (Input.GetKey(KeyCode.S))
                nextState = State.Idle;
            if (Input.GetKey(KeyCode.A))
                nextState = State.WalkingBackward;

            if (Input.GetKeyUp(KeyCode.D))
            {
                nextState = State.Idle;
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                nextState = State.Idle;
            }

            if (Input.GetKey(KeyCode.Alpha1))
                SelectedAttackIndex = 0;
            if (Input.GetKeyDown(KeyCode.Alpha2))
                SelectedAttackIndex = 1;
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

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (IsGrounded())
                {
                    Jump();
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

    public void PhysicsUpdate()
    {
        if (IsGrounded())
        {
            rigidbody.gravityScale = 0;
        }
        else
        {
            rigidbody.gravityScale = AirGravity;
        }

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
        
        animator.SetInteger("IsMoving", 1);
        SetChildAnimations("IsMoving", 1);
        if (!PlayerControlled)
            TurnedAround = true;
    }

    public void Jump()
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
        rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
    public void Attack(Move selectedMove)
    {
        
        if ((ActiveMove == null || ActiveMove.canExitAttack) && TimeStunned <= 0)
        {
            EndAttack();
            var move = Instantiate(selectedMove, gameObject.transform);
            if (!move.CanMove)
            {
                SetIdle();
            }
            move.AttachedExemon = gameObject;
            move.moveTime = 0;
            ActiveMove = move;
            animator.SetInteger("MoveID", move.MoveID);
            ActiveMove.InitiateAttack();
        }
        
    }


    void SetIdle()
    {

        if (IsGrounded())
        {
            rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
        }
        animator.SetInteger("IsMoving", 0);
        SetChildAnimations("IsMoving", 0);
    }


    public void EndAttack()
    {
        ActiveMove = null;
        animator.SetInteger("MoveID", 0);
        AimLocked = false;

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


    public IEnumerator LockAim(AimLockedTime aimLockedTime)
    {
        yield return new WaitForSeconds(aimLockedTime.TimeStart);

        AimLocked = true;

        AimLockedPosition = target;
        yield return new WaitForSeconds(aimLockedTime.Duration);
        AimLocked = false;

    }

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


[System.Serializable]
public struct AimLockedTime { public float TimeStart; public float Duration; }






