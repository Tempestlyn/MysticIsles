using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BattleScene : MonoBehaviour
{


    public GameObject ExemonStartPosition1;
    public GameObject ExemonStartPosition2;

    public BattleExemon P1Exemon;
    public BattleExemon P2Exemon;

    public TMP_Text PlayerHealth;
    public TMP_Text EnemyHealth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (P2Exemon != null)
        {
            EnemyHealth.text = "HP: " + P2Exemon.health.ToString();
        }
    }
}
