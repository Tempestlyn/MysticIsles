using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleExemon : MonoBehaviour
{
    public bool PlayerControlled;

    public BaseExemon exemon;
    public Stance stance;
    public GameObject move1;
    private Rigidbody2D rigidbody;
    private Animator animator;
    public float speed;

    public BoxCollider footAttack;
    public BoxCollider headAttack;
    public BoxCollider tailAttack;
    

    public GameObject enemyExemon;
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

            Move();

        if (PlayerControlled)
        {
            if (Input.GetKeyDown(KeyCode.A))
                stance = Stance.Attack;
            if (Input.GetKeyDown(KeyCode.D))
                stance = Stance.Defend;
            if (Input.GetKeyDown(KeyCode.S))
                stance = Stance.StayAway;
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
            }

            


            }


        if (Mathf.Abs(rigidbody.velocity.x) >= .1)
        {
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }



    }

    public enum Stance
    {
        Attack,
        Defend,
        StayAway,
        
    }


