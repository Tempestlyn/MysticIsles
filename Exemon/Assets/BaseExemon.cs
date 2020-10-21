using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Exemon", menuName = "Exemon/Create new exemon")]
public class BaseExemon : ScriptableObject
{

    public string Name;
    public PowerType[] types;
    public float baseHP;
    public float baseAttack;
    public float baseSpecialAttack;
    public float baseDefence;
    public float baseSpecialDefence;
    public float baseSpeed;
    public float Obedience;

    private float hpIV;
    private float attackIV;
    private float defenceIV;
    private float specialAttackIV;
    private float specialDefenceIV;
    private float speedIV;
    private int attackBuffs;
    private int defenceBuffs;
    private int specialAttackBuffs;
    private int specialDefenceBuffs;
    private int speedBuffs;
    public void InitializeExemon()
    {
        for (int i = 0; i <= 64; i++)
        {
            var scoreIndex = UnityEngine.Random.Range(0, 6);

            switch (scoreIndex)
            {
                case 0:
                    hpIV += 1;
                    break;
                case 1:
                    attackIV += 1;
                    break;
                case 2:
                    defenceIV += 1;
                    break;
                case 3:
                    specialAttackIV += 1;
                    break;
                case 4:
                    specialDefenceIV += 1;
                    break;
                case 5:
                    speedIV += 1;
                    break;
            }

        }

        Obedience = 0;

        attackBuffs = 0;
        defenceBuffs = 0;
        specialDefenceBuffs = 0;
        specialAttackBuffs = 0;
        speedBuffs = 0;
        
    }



}


public enum PowerType
{
Fire, 
Water,
Plant,
Earth,
Poison,
Lightning,
Air,
Frost,
Physical,
Arcane,
Light,
Dark
}



