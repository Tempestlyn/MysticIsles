using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class IslandBuilder : MonoBehaviour
{
    [SerializeField]
    public List<IslandBlock> IslandPieceBank = new List<IslandBlock>();

    public IslandBlock SelectedIslandBlock;
    public GameObject SelectedPieceOverlay;
    public float xSnapInterval;
    public float ySnapInterval;
    public Vector2 basePiece;

    public bool TryingToPlacePiece = false;
    public bool SettingBase;

    [SerializeField]
    public PieceData Data;


    public void SavePiece()
    {
        Debug.Log("Test1");
        string json = JsonUtility.ToJson(Data);
        SaveSystem.SavePiece(json);
    }
    public void LoadSavedPieces()
    {


    }

    private void Start()
    {
        SaveSystem.Init();
        SwapSelectedPiece(1);

    }

    private void Update()
    {
        if (SelectedIslandBlock != null)
        {
            var canPlaceBlock = false;

            var x = Mathf.Round(Camera.main.ScreenToWorldPoint(Input.mousePosition).x / xSnapInterval) * xSnapInterval;
            var y = Mathf.Round(Camera.main.ScreenToWorldPoint(Input.mousePosition).y / ySnapInterval) * ySnapInterval;

            Vector3 position = new Vector2(x, y);

            if (SelectedPieceOverlay != null)
            {
                SelectedPieceOverlay.transform.position = position;
            }
            if (Input.GetKey(KeyCode.Mouse0))
            {

                if (SettingBase)
                {

                }
                    PlacePiece(SelectedIslandBlock.gameObject, position);
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null && hit.collider.gameObject.GetComponent<IslandBlock>())
                {
                    Destroy(hit.collider.gameObject);
                }
            }
        }  


    }
    public void PlacePiece(GameObject Block, Vector2 position)
    {
        var prefabData = Block.GetComponent<IslandBlock>();
        var canPlace = true;
        foreach (Vector2 FilledPosition in prefabData.FilledCoords)
        {

            if (Data.FilledCords.Contains(position + FilledPosition)){
                canPlace = false;
            }
            
        }
        if (Data.FilledCords.Contains(position))
        {
            canPlace = false;
        }
        if (canPlace)
        {
            
            Instantiate(Block, new Vector3(position.x, position.y, 0), Quaternion.identity);
            var blockData = new BlockData();
            blockData.position = position;
            blockData.BlockID = prefabData.BlockID;
            blockData.FilledCoords = new List<Vector2>();
  

            Data.FilledCords.Add(position);
            Data.blockData.Add(blockData);
            foreach(Vector2 coords in prefabData.FilledCoords)
            {
                Debug.Log(coords);
                blockData.FilledCoords.Add(coords);
                Data.FilledCords.Add(position + coords);
            }
        }
    }

    public void FillPiece(IslandBlock islandBlock)
    {
        
    }

    

    public void SwapSelectedPiece(int ID)
    {
        for (var i = 0; i < IslandPieceBank.Count; i++)
        {
            if (IslandPieceBank[i].BlockID == ID)
            {
                SelectedIslandBlock = IslandPieceBank[i];
            }
        }
    }


    [System.Serializable]
    public class BlockData
    {
        public Vector2 position;
        public List<Vector2> FilledCoords;
        public int BlockID;
    }
    [System.Serializable]
    public class PieceData
    {
        public List<BlockData> blockData;
        public List<Vector2> FilledCords;
    }


 
}
