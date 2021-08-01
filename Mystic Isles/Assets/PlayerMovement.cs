using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : OverworldExemon
{
    public float Speed;
    private Rigidbody myRigidbody;
    public float Gravity;
    public Camera PlayerCamera;
    public bool CanBattle;
    public BaseBattleEntity BaseMage;
    public Animator spriteAnimator;

    public GameObject BattleSystem;
    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        PlayerCamera = Camera.main;
    }
    void Update()
    {
        Movement();
        myRigidbody.AddRelativeForce(Vector3.down * Gravity);
    }

    void Movement()
    {
            
            if (Input.GetKeyDown(KeyCode.D))
            {
                spriteAnimator.SetInteger("MoveDirection", 2);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {

                spriteAnimator.SetInteger("MoveDirection", 2);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                spriteAnimator.SetInteger("MoveDirection", 3);
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                spriteAnimator.SetInteger("MoveDirection", 1);
            }
            if (!Input.GetKeyDown(KeyCode.A))
            {

            }
            if (!Input.anyKey)
            {
                spriteAnimator.SetInteger("MoveDirection", 0);
            }

            float hor = Input.GetAxisRaw("Horizontal");
            float ver = Input.GetAxisRaw("Vertical");
            Vector3 playerMovement = new Vector3(hor, 0, ver).normalized * Speed * Time.deltaTime;
            transform.Translate(playerMovement, Space.Self);
        
    }
     
    void InitiateBattle(BaseBattleEntity enemyExemon)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        var battleData = Instantiate(BattleSystem).GetComponent<BattleData>();
        battleData.P1Exemon = BaseMage.BattleEntity;
        battleData.P2Exemon = enemyExemon.BattleEntity;
        OverworldData.Instance.SaveWorldState();
        SceneManager.LoadScene("BattleScene");
    }

    void OnCollisionEnter(Collision collider)
    {
        
        if (collider.gameObject.GetComponent<OverworldExemon>())
        {
            var enemy = collider.gameObject.GetComponent<OverworldExemon>().Exemon;
            Destroy(collider.gameObject);
            OverworldData.Instance.ExistingOverworldEntities.Remove(enemy);
            InitiateBattle(enemy);
        }
        
        
    }
}