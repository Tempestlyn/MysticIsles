using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;
using UnityEngine.SceneManagement;

public class BattleData : MonoBehaviour
{
    private bool BattleStarted;
    private bool EnemyDefeated;
    
    public GameObject ExemonStartPosition1;
    public GameObject ExemonStartPosition2;

    public GameObject P1Exemon;
    public GameObject P2Exemon;

    public TMP_Text PlayerHealth;
    public TMP_Text EnemyHealth;

    public List<BattleExemon> enemyEntities = new List<BattleExemon>();

    public Camera BattleCam;
    public CinemachineVirtualCamera VC;

    public GameObject BattleEndMenu;
    // Start is called before the first frame update

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EnemyDefeated = true;
        foreach (BattleExemon entity in enemyEntities)
        {
            if (entity.health > 0)
            {
                EnemyDefeated = false;
            }

        }

        if (EnemyDefeated && BattleStarted)
        {
            EndBattle();
        }
        var enemyData = P2Exemon.GetComponent<BattleExemon>();
        if (enemyData.health <= 0)
        {
            enemyData.health = 0;
            EndBattle();
        }

  
        
    }


    public void EndBattle()
    {
        BattleEndMenu.SetActive(true);
        
    }

    public void ExitBattle()
    {
        SceneManager.LoadScene("SampleScene");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Destroy(gameObject);
    }
    
    public void InitiateBattle(Transform playerStart, Transform enemyStart)
    {
        
        var player = P1Exemon.GetComponent<BattleExemon>();
        player.BattleSystem = this;
        player.PlayerControlled = true;
        var SpawnedPlayer = Instantiate(P1Exemon, playerStart);
        SpawnedPlayer.transform.position = new Vector2(playerStart.transform.position.x, SpawnedPlayer.transform.position.y + (SpawnedPlayer.GetComponent<Collider2D>().bounds.size.y / 2));
        VC.Follow = SpawnedPlayer.transform;
        VC.m_Lens.OrthographicSize = 15;


        var enemy = P2Exemon.GetComponent<BattleExemon>();
        enemy.BattleSystem = this;
        enemy.PlayerControlled = false;
        enemy.enemyExemon = SpawnedPlayer;
        var spawnedEnemy = (Instantiate(P2Exemon, enemyStart.transform).GetComponent<BattleExemon>());
        spawnedEnemy.transform.position = new Vector2(enemyStart.transform.position.x, spawnedEnemy.transform.position.y + (spawnedEnemy.GetComponent<Collider2D>().bounds.size.y / 2));
        enemyEntities.Add(spawnedEnemy);

        BattleStarted = true;
        
    }
}
