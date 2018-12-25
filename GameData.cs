using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameData
{
    //Player Stats
    public float health = 100;
    public Vector3 location;
    public Quaternion rotation;
    public bool cheatsEnabled;
    public int Scene;

    //Progress - Objectives
    //public Objective[] objectivesArray;

    public GameData(float health = 100, bool cheatsEnabled = false)
    {
        this.health = health;
        this.cheatsEnabled = cheatsEnabled;
    }
}