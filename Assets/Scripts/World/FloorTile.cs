using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTile : MonoBehaviour {

    public Vector3 position;
    public bool hasWalked = false;

	// Use this for initialization
	void Start () {
        position = transform.position;
    }
}
