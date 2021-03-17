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
    public ElementalType[] types;
    public float baseHP;
    public float baseAttack;
    public float baseSpecialAttack;
    public float baseDefence;
    public float baseSpecialDefence;
    public float baseSpeed;
    public float Obedience;

    public float Reach;
    private float hpIV;

    private float physicalMelee;
    private float physicalRanged;
    private float physicalDefence;
    private float elementalMelee;
    private float elementalRanged;
    private float elementalDefence;
    private float speed;

    public GameObject exemon;

    public Stance defaultStance;
    public Animation[] AttackAnimations;


    public void InitializeExemon()
    {
        for (int i = 0; i <= 64; i++)
        {
            var scoreIndex = UnityEngine.Random.Range(0, 6);

            switch (scoreIndex)
            {
                case 0:
                    physicalMelee += 1;
                    break;
                case 1:
                    physicalRanged += 1;
                    break;
                case 2:
                    physicalDefence += 1;
                    break;
                case 3:
                    elementalMelee += 1;
                    break;
                case 4:
                    elementalRanged += 1;
                    break;
                case 5:
                    elementalDefence += 1;
                    break;
                case 6:
                    speed += 1;
                    break;
            }

        }

        Obedience = 0;

        
    }

    


}


public enum ElementalType
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



