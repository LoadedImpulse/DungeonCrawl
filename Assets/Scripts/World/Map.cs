using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    public struct TileCoords {
        public int xPos;
        public int yPos;
        public bool floor;
    }

    public GameObject floorTile;
    public int ChanceForTile;
    
    public List<GameObject> ListOfFloorTiles = new List<GameObject>();
    private List<TileCoords> dungeonMap = new List<TileCoords>();

	// Use this for initialization
	void Start () {
        ChanceForTile = 35;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.R)) {
            ClearDungeon();
            CreateDungeon();
        }
        if (Input.GetKeyUp(KeyCode.F)) {
            /*
            GameObject chosen = ListOfFloorTiles[Random.Range(0, ListOfFloorTiles.Count)];
            Debug.Log("Starting tile: " + chosen);
            GameObject end = FindEndOfFloor(chosen, new List<GameObject>());
            */
            ConnectFloorTiles();
        }
	}

    void ClearDungeon() {
        foreach (var item in ListOfFloorTiles) {
            Destroy(item);
        }
        ListOfFloorTiles.Clear();
        dungeonMap.Clear();
    }
    void GenerateLayout() {
        for (int x = 0; x < Variables.mapXWidth; x++) {
            for (int y = 0; y < Variables.mapYWidth; y++) {
                bool generateFloor = false;
                if (Random.Range(1,100) < ChanceForTile) {
                    generateFloor = true;
                }
                dungeonMap.Add(new TileCoords { xPos = x, yPos = y, floor = generateFloor });
            }
        }
    }
    void CreateFloor() {

        for (int x = 0; x < Variables.mapXWidth; x++) {
            for (int y = 0; y < Variables.mapYWidth; y++) {
                foreach (var item in dungeonMap) {
                    if (item.xPos == x && item.yPos == y && item.floor == true) {
                        GameObject go = Instantiate(floorTile, new Vector3((x * Variables.unityUnit), 0, (y * Variables.unityUnit)), Quaternion.identity);
                        go.name = "FloorTile[" + x + "," + y + "]";

                        go.transform.SetParent(this.transform);
                        ListOfFloorTiles.Add(go);
                    }
                }
               
            }
        }
    }

    void ConnectFloorTiles() {

        for (int i = 0; i < ListOfFloorTiles.Count; i++) {
            Debug.Log(ListOfFloorTiles[i].transform.position);
            GameObject lastTile = FindEndOfFloor(ListOfFloorTiles[i], new List<GameObject>());
            
            if (!GetTileAtPos(lastTile.transform.position.x + Variables.unityUnit, lastTile.transform.position.z)) {
                GameObject go = Instantiate(floorTile, new Vector3(lastTile.transform.position.x + Variables.unityUnit, lastTile.transform.position.y, lastTile.transform.position.z), Quaternion.identity);
                go.name = "ConnectTile [" + go.transform.position.x + "," + go.transform.position.z + "]";
                ListOfFloorTiles.Add(go);
            }
            else if (!GetTileAtPos(lastTile.transform.position.x - Variables.unityUnit, lastTile.transform.position.z)) {
                GameObject go = Instantiate(floorTile, new Vector3(lastTile.transform.position.x - Variables.unityUnit, lastTile.transform.position.y, lastTile.transform.position.z), Quaternion.identity);
                go.name = "ConnectTile [" + go.transform.position.x + "," + go.transform.position.z + "]";
                ListOfFloorTiles.Add(go);
            }
            else if (!GetTileAtPos(lastTile.transform.position.x, lastTile.transform.position.z + Variables.unityUnit)) {
                GameObject go = Instantiate(floorTile, new Vector3(lastTile.transform.position.x, lastTile.transform.position.y, lastTile.transform.position.z + Variables.unityUnit), Quaternion.identity);
                go.name = "ConnectTile [" + go.transform.position.x + "," + go.transform.position.z + "]";
                ListOfFloorTiles.Add(go);
            }
            else if (!GetTileAtPos(lastTile.transform.position.x, lastTile.transform.position.z - Variables.unityUnit)) {
                GameObject go = Instantiate(floorTile, new Vector3(lastTile.transform.position.x, lastTile.transform.position.y, lastTile.transform.position.z - Variables.unityUnit), Quaternion.identity);
                go.name = "ConnectTile [" + go.transform.position.x + "," + go.transform.position.z + "]";
                ListOfFloorTiles.Add(go);
            }
        } 
    }
    GameObject FindEndOfFloor(GameObject startTile, List<GameObject> checkedTiles) {
        //Debug.Log("CurrentTile: " + startTile);
        if (GetTileAtPos(startTile.transform.position.x + Variables.unityUnit,startTile.transform.position.z) &&!checkedTiles.Contains(GetTileAtPos(startTile.transform.position.x + Variables.unityUnit, startTile.transform.position.z))) {
            checkedTiles.Add(startTile);
            FindEndOfFloor(GetTileAtPos(startTile.transform.position.x + Variables.unityUnit, startTile.transform.position.z),checkedTiles);
        }
        else if (GetTileAtPos(startTile.transform.position.x - Variables.unityUnit, startTile.transform.position.z) && !checkedTiles.Contains(GetTileAtPos(startTile.transform.position.x - Variables.unityUnit, startTile.transform.position.z))) {
            checkedTiles.Add(startTile);
            FindEndOfFloor(GetTileAtPos(startTile.transform.position.x - Variables.unityUnit, startTile.transform.position.z), checkedTiles);
        }
        else if (GetTileAtPos(startTile.transform.position.x, startTile.transform.position.z + Variables.unityUnit) && !checkedTiles.Contains(GetTileAtPos(startTile.transform.position.x, startTile.transform.position.z + Variables.unityUnit))) {
            checkedTiles.Add(startTile);
            FindEndOfFloor(GetTileAtPos(startTile.transform.position.x, startTile.transform.position.z + Variables.unityUnit), checkedTiles);
        }
        else if (GetTileAtPos(startTile.transform.position.x, startTile.transform.position.z - Variables.unityUnit) && !checkedTiles.Contains(GetTileAtPos(startTile.transform.position.x, startTile.transform.position.z - Variables.unityUnit))) {
            checkedTiles.Add(startTile);
            FindEndOfFloor(GetTileAtPos(startTile.transform.position.x, startTile.transform.position.z - Variables.unityUnit), checkedTiles);
        }
        else {
            //Debug.Log("EndOfFloor: " + startTile);
            return startTile;
        }
        return startTile;
        //Debug.Log("ERROR");
    }
    GameObject GetTileAtPos(float x, float y) {
        foreach (var tile in ListOfFloorTiles) {
            if (tile.transform.position.x == x && tile.transform.position.z == y) {
                return tile;
            }
        }
        //Debug.Log("ERROR");
        return null;
    }

    void PlacePlayer() {
        int startPosition = Random.Range(0, ListOfFloorTiles.Count);
        Vector3 playerPosition = new Vector3(ListOfFloorTiles[startPosition].transform.position.x, 1, ListOfFloorTiles[startPosition].transform.position.z);
        ListOfFloorTiles[startPosition].GetComponent<FloorTile>().hasWalked = true;
        GameObject.Find("Main Camera").transform.position = playerPosition;
    }

    void CreateDungeon() {
        GenerateLayout();
        CreateFloor();
        PlacePlayer();
    }
}
