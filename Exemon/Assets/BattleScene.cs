using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;

public class BattleScene : MonoBehaviour
{


    public GameObject ExemonStartPosition1;
    public GameObject ExemonStartPosition2;

    public BattleExemon P1Exemon;
    public BattleExemon P2Exemon;

    public TMP_Text PlayerHealth;
    public TMP_Text EnemyHealth;

    public Camera Battle;
    public Camera OverworldCam;
    public static Camera BattleCam;
    public CinemachineVirtualCamera VC;

    public GameObject BattleEndMenu;
    // Start is called before the first frame update
    void Start()
    {
        BattleCam = Battle;

        VC.Follow = P1Exemon.transform;
        VC.m_Lens.OrthographicSize = 18;
    }

    // Update is called once per frame
    void Update()
    {
        if (P2Exemon.health <= 0)
        {
            P2Exemon.health = 0;
            EndBattle();
        }

        if (P2Exemon != null)
        {
            EnemyHealth.text = "HP: " + P2Exemon.health.ToString();
        }
        if (P1Exemon != null)
        {
            PlayerHealth.text = "HP: " + P1Exemon.health.ToString();
        }
        
        
    }


    public void EndBattle()
    {
        BattleEndMenu.SetActive(true);
        
    }

    public void ExitBattle()
    {
        BattleEndMenu.SetActive(false);
        gameObject.SetActive(false);
        OverworldCam.gameObject.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
