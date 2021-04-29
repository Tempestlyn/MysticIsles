using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed;
    private Rigidbody myRigidbody;
    public float Gravity;
    public Camera PlayerCamera;
    public GameObject BattleScene;
    public bool CanBattle;
    public BaseExemon BaseMage;
    public List<BaseExemon> Exemon = new List<BaseExemon>();
    public Animator spriteAnimator;

    private GameObject Robe;
    private void Start()
    {
        if (BaseMage.Robe != null)
        {
            Robe = Instantiate(BaseMage.Robe, transform);
            Robe.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        myRigidbody = GetComponent<Rigidbody>();
        
    }
    void Update()
    {
        Movement();
        myRigidbody.AddRelativeForce(Vector3.down * Gravity);
    }

    void Movement()
    {
        if (!BattleScene.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                spriteAnimator.SetInteger("MoveDirection", 2);
                if (Robe != null)
                {
                    Robe.gameObject.GetComponent<Animator>().SetInteger("IsMoving", 1);
                    Robe.gameObject.transform.rotation = Quaternion.Euler(0, PlayerCamera.transform.rotation.eulerAngles.y, 0);
                }
                spriteAnimator.gameObject.transform.rotation = Quaternion.Euler(0, PlayerCamera.transform.rotation.eulerAngles.y, 0);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {

                spriteAnimator.SetInteger("MoveDirection", 2);
                spriteAnimator.gameObject.transform.rotation = Quaternion.Euler(0, PlayerCamera.transform.rotation.eulerAngles.y + 180, 0);
                if (Robe != null)
                {
                    Robe.gameObject.GetComponent<Animator>().SetInteger("IsMoving", 1);
                    Robe.gameObject.transform.rotation = Quaternion.Euler(0, PlayerCamera.transform.rotation.eulerAngles.y + 180, 0);
                }
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                spriteAnimator.SetInteger("MoveDirection", 3);
                //spriteAnimator.gameObject.transform.rotation = new Quaternion(spriteAnimator.transform.rotation.x, 0, spriteAnimator.transform.rotation.z, spriteAnimator.transform.rotation.w);
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                spriteAnimator.SetInteger("MoveDirection", 1);
                //spriteAnimator.gameObject.transform.rotation = new Quaternion(spriteAnimator.transform.rotation.x, 0, spriteAnimator.transform.rotation.z, spriteAnimator.transform.rotation.w);
            }
            if (!Input.GetKeyDown(KeyCode.A))
            {

            }
            if (!Input.anyKey)
            {
                if (Robe != null)
                {
                    Robe.gameObject.GetComponent<Animator>().SetInteger("IsMoving", 0);
                }
                spriteAnimator.SetInteger("MoveDirection", 0);
                //spriteAnimator.gameObject.transform.rotation = new Quaternion(spriteAnimator.transform.rotation.x, 0, spriteAnimator.transform.rotation.z, spriteAnimator.transform.rotation.w);
            }

            float hor = Input.GetAxisRaw("Horizontal");
            float ver = Input.GetAxisRaw("Vertical");
            Vector3 playerMovement = new Vector3(hor, 0, ver).normalized * Speed * Time.deltaTime;
            transform.Translate(playerMovement, Space.Self);
        }
    }

    void InitiateBattle(BaseExemon enemyExemon)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        BattleScene.SetActive(true);
        PlayerCamera.gameObject.SetActive(false);

        var PlayerExemon = new GameObject();

        PlayerExemon = Instantiate(BaseMage.exemon, BattleScene.GetComponent<BattleScene>().ExemonStartPosition1.transform);
        
        if (BaseMage.Robe != null)
        {
            var battleRobe = Instantiate(BaseMage.Robe, PlayerExemon.transform);
        }
        var battleExemon = PlayerExemon.GetComponent<BattleExemon>();
        BattleScene.GetComponent<BattleScene>().P1Exemon = battleExemon;
        battleExemon.PlayerControlled = true;
        battleExemon.reach = BaseMage.Reach;
        /*
        foreach (BaseExemon baseExemon in Exemon)
        {


                PlayerExemon = Instantiate(baseExemon.exemon, BattleScene.GetComponent<BattleScene>().ExemonStartPosition1.transform);
                
                var battleExemon = PlayerExemon.GetComponent<BattleExemon>();
                BattleScene.GetComponent<BattleScene>().P1Exemon = battleExemon;
                battleExemon.PlayerControlled = true;
                battleExemon.reach = baseExemon.Reach;

                
            
        }
        */
        var EnemyExemon = Instantiate(enemyExemon.exemon, BattleScene.GetComponent<BattleScene>().ExemonStartPosition2.transform);
        PlayerExemon.GetComponent<BattleExemon>().enemyExemon = EnemyExemon;
        var enemyBattleExemon = EnemyExemon.GetComponent<BattleExemon>();
        BattleScene.GetComponent<BattleScene>().P2Exemon = enemyBattleExemon;
        BattleScene.GetComponent<BattleScene>().P1Exemon = PlayerExemon.GetComponent<BattleExemon>();
        BattleScene.GetComponent<BattleScene>().OverworldCam = PlayerCamera;
        enemyBattleExemon.reach = enemyExemon.Reach;
        enemyBattleExemon.enemyExemon = PlayerExemon;

    }

    void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.GetComponent<OverworldExemon>() != null)
        {

            InitiateBattle(collider.gameObject.GetComponent<OverworldExemon>().Exemon);
            Destroy(collider.gameObject);
        }

        
    }
}