﻿using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;
public class BaseExemon : MonoBehaviour
{
    public float Rarity;

    public string Name;
    public Sprite Image;

    public PokemonType type;
    public float baseHP;
    private float maxHP;
    public float baseAttack;
    private float maxAttack;
    public float baseSpecialAttack;
    private float maxSpecialAttack;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    void OnCollisionEnter(Collision collision)
    {
        var Player = collision.gameObject.GetComponent<PlayerMovement>();

        if (Player != null)
        {
            
        }

    }

}


public enum PokemonType
{
Fire, 
Water,
Plant,
Earth,
Lightning,
Air,
Frost,
Physical,
Arcane,
Light,
Dark
}
