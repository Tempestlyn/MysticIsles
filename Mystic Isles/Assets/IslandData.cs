using System.Collections;
using System.Collections.Generic;
using UnityEngine; 


//[CreateAssetMenu(fileName = "NewIsland", menuName = "Island", order = 1)]
[System.Serializable]
public class IslandData
{
    public int IslandWidth;
    public int IslandHeight;
    public IslandBlock StartingPiece;
}

