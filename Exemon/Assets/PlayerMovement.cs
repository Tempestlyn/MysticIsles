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
    
    public List<BaseExemon> Exemon = new List<BaseExemon>();

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        Movement();
        myRigidbody.AddRelativeForce(Vector3.down * Gravity);
    }

    void Movement()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");
        Vector3 playerMovement = new Vector3(hor, 0, ver).normalized * Speed * Time.deltaTime;
        transform.Translate(playerMovement, Space.Self);
    }

    void InitiateBattle(BaseExemon enemyExemon)
    {
        BattleScene.SetActive(true);
        PlayerCamera.gameObject.SetActive(false);

        var PlayerExemon = new GameObject();

        foreach (BaseExemon baseExemon in Exemon)
        {
            Debug.Log(baseExemon);
            if (baseExemon.exemon.GetComponent<BattleExemon>().health > 0)
            {
                PlayerExemon = Instantiate(baseExemon.exemon, BattleScene.GetComponent<BattleScene>().ExemonStartPosition1.transform);
                
                var battleExemon = PlayerExemon.GetComponent<BattleExemon>();
                BattleScene.GetComponent<BattleScene>().P1Exemon = battleExemon;
                battleExemon.PlayerControlled = true;
                battleExemon.reach = baseExemon.Reach;

                
            }
        }
        var EnemyExemon = Instantiate(enemyExemon.exemon, BattleScene.GetComponent<BattleScene>().ExemonStartPosition2.transform);
        PlayerExemon.GetComponent<BattleExemon>().enemyExemon = EnemyExemon;
        var enemyBattleExemon = EnemyExemon.GetComponent<BattleExemon>();
        BattleScene.GetComponent<BattleScene>().P2Exemon = enemyBattleExemon;
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