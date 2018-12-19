using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurScene : MonoBehaviour
{
    GameObject Player;
    public GameObject border;

	void Start ()
    {
        Player = GameObject.Find("ThirdPersonController");
        if (Player.GetComponent<ProgressManager>().foughtMinotaur.completed == false)
        {
            Player.transform.position = GameObject.Find("MinotaurSceneSpawn").transform.position;
            Player.transform.rotation = GameObject.Find("MinotaurSceneSpawn").transform.rotation;
            border.SetActive(true);
        }
        else if (Player.GetComponent<ProgressManager>().wakeUpScene.completed == true)//If bed scene has already been done
        {
            gameObject.GetComponent<MinotaurScene>().enabled = false;
            GameObject.Find("Percy").GetComponent<BedScene>().enabled = true;
        }
    }
	
	void Update ()
    {
        //If Minotaur is killed
        if (GameObject.Find("Minotaur(Story)") == null)
        {
            Player.GetComponent<ProgressManager>().foughtMinotaur.completed = true;
            border.SetActive(false);

            GameObject.Find("Percy").GetComponent<BedScene>().enabled = true;
            gameObject.GetComponent<MinotaurScene>().enabled = false;
        }
    }
}