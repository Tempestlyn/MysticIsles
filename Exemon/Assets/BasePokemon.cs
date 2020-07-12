using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BasePokemon : MonoBehaviour
{
    public float Rarity;

    public string Name;
    public Sprite Image;

    public PokemonType type;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
