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
        ChanceForTile = 50;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.R)) {
            ClearDungeon();
            CreateDungeon();
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
        int debugCounter = 0;
        for (int x = 0; x < Variables.mapXWidth; x++) {
            for (int y = 0; y < Variables.mapYWidth; y++) {
                bool generateFloor = false;
                if (Random.Range(1,100) < ChanceForTile) {
                    generateFloor = true;
                    debugCounter++;
                }

                dungeonMap.Add(new TileCoords { xPos = x, yPos = y, floor = generateFloor });
            }
        }
        Debug.Log("Floor tiles: " + debugCounter + " out of: " + Variables.mapXWidth * Variables.mapYWidth);
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
