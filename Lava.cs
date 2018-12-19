using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    GameObject rockWall;
    RockWall rockWallScript;
	void Start ()
    {
        rockWall = GameObject.Find("RockWall");
        rockWallScript = rockWall.GetComponent<RockWall>();
    }
	
	void Update ()
    {
        //Makes the lava resize - flowing down the wall
        if (rockWallScript.onWall == true && transform.localScale.z < 20)
        {
            transform.localScale += new Vector3(0, 0, Random.Range(rockWallScript.lavaMinSpeed, rockWallScript.lavaMaxSpeed));

        }
    }
}