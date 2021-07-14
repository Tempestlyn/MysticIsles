using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEntitySpawn : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject PlayerSpawn;
    public GameObject EnemySpawn;
    public GameObject EndBattleUI;
    public BattleData battleData;
    private void Awake()
    {
        battleData = FindObjectOfType<BattleData>();
        battleData.BattleEndMenu = EndBattleUI;
        battleData.InitiateBattle(PlayerSpawn.transform, EnemySpawn.transform);
    }

    public void EndBattle()
    {
        battleData.ExitBattle();
    }
}
