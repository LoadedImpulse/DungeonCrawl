using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    enum directions {
        north,
        south,
        west,
        east,
        up,
        down,
        left,
        right,
        rotateLeft,
        rotateRight
    }
    Map gameMap;
	// Use this for initialization
	void Start () {
        gameMap = GameObject.Find("GameWorld").GetComponent<Map>();
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.W)) {
            Walk(directions.up);
        }
        else if (Input.GetKeyUp(KeyCode.S)) {
            Walk(directions.down);
        }
        else if (Input.GetKeyUp(KeyCode.A)) {
            Walk(directions.left);
        }
        else if (Input.GetKeyUp(KeyCode.D)) {
            Walk(directions.right);
        }
        else if(Input.GetKeyUp(KeyCode.Q)) {
            Rotate(directions.rotateLeft);
        }
        else if (Input.GetKeyUp(KeyCode.E)) {
            Rotate(directions.rotateRight);
        }
    }

    void Rotate(directions direction) {
        if(direction == directions.rotateRight) {
            this.transform.Rotate(Vector3.up,90f);
        }
        else {
            this.transform.Rotate(Vector3.up, -90f);
        }

      
    }
    
    directions CheckRotation() {
        float yRotation = this.transform.rotation.eulerAngles.y;
        Debug.Log("yRotation: " + yRotation);
        if (yRotation == 0f) {
            Debug.Log("Facing north");
            return directions.north;
        }
        else if (yRotation == 270f) {
            Debug.Log("Facing west");
            return directions.west;
        }
        else if (yRotation == 180f) {
            Debug.Log("Facing south");
            return directions.south;
        }
        else {
            Debug.Log("Facing east");
            return directions.east;
        }
        
    }
    
    void Walk(directions direction) {
        Vector3 newPosition = new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z);
        directions rotation = CheckRotation();
        switch (direction) {
            case directions.up:
                if (rotation == directions.north) {
                    newPosition.z += Variables.unityUnit;
                }
                else if (rotation == directions.south) {
                    newPosition.z -= Variables.unityUnit;
                }
                else if (rotation == directions.west) {
                    newPosition.x -= Variables.unityUnit;
                }
                else {
                    newPosition.x += Variables.unityUnit;
                }
                break;
            case directions.down:
                if (rotation == directions.north) {
                    newPosition.z -= Variables.unityUnit;
                }
                else if (rotation == directions.south) {
                    newPosition.z += Variables.unityUnit;
                }
                else if (rotation == directions.west) {
                    newPosition.x += Variables.unityUnit;
                }
                else {
                    newPosition.x -= Variables.unityUnit;
                }
                break;
            case directions.left:
                if (rotation == directions.north) {
                    newPosition.x -= Variables.unityUnit;
                }
                else if (rotation == directions.south) {
                    newPosition.x += Variables.unityUnit;
                }
                else if (rotation == directions.west) {
                    newPosition.z -= Variables.unityUnit;
                }
                else {
                    newPosition.z += Variables.unityUnit;
                }
                break;
            case directions.right:
                if (rotation == directions.north) {
                    newPosition.x += Variables.unityUnit;
                }
                else if (rotation == directions.south) {
                    newPosition.x -= Variables.unityUnit;
                }
                else if (rotation == directions.west) {
                    newPosition.z += Variables.unityUnit;
                }
                else {
                    newPosition.z -= Variables.unityUnit;
                }
                break;
            default:
                break;
        }
        
        if (newPosition.x <= ((Variables.mapXWidth - 1) * Variables.unityUnit) && newPosition.z <= ((Variables.mapYWidth - 1) * Variables.unityUnit)) {
            if(newPosition.x < 0 || newPosition.z < 0) {
                return;
            }
           
        }
        
        foreach (var item in gameMap.ListOfFloorTiles) {
            FloorTile floorTile = item.GetComponent<FloorTile>();
            if (floorTile.position == new Vector3(newPosition.x, 0, newPosition.z)) {
                floorTile.hasWalked = true;
                this.transform.position = newPosition;
                Debug.Log("Moved to position: " + newPosition);
            }
        }
    }

}
