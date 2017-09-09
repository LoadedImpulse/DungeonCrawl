using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    private Dictionary<string, int> characterStats = new Dictionary<string, int>();
    // Use this for initialization
    void Start () {
        InitializeCharacterStats();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void InitializeCharacterStats(){
        string skillList = "";
        foreach (string item in Variables.stats) {
            characterStats.Add(item,Random.Range(Variables.minLevel, Variables.maxLevel));
            skillList += "Skill: " + item + " level: " + characterStats[item] + ", ";
        }
        Debug.Log("Skills initialized");
        Debug.Log(skillList);
    }

    int GetLevel(string skill) {
        return characterStats[skill];
    }
    void SetLevel(string skill, int level) {
        if(characterStats[skill] < Variables.maxLevel) {
            characterStats[skill] = level;
        }
    }
    
}
