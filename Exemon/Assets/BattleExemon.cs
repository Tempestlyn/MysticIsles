using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleExemon : MonoBehaviour
{

    public BaseExemon exemon;
    public Stance stance;
    public GameObject move1;
    
    // Start is called before the first frame update


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}

public enum Stance
{
    Attack,
    Defend,
    StayAway
}