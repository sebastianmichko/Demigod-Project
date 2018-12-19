using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StyxObject : MonoBehaviour
{
    public int zMax = 500;
    public GameObject StyxSpawner;
    public void Start()
    {
        StyxSpawner = GameObject.Find("Styx Spawner");
    }
    void Update ()
    {
        if (gameObject.transform.position.z > zMax)
        {
            Destroy(gameObject);
            if(gameObject.name == "Teddy Bear") StyxSpawner.GetComponent<StyxRiver>().teddyBears.Remove(gameObject.transform);
            else if(gameObject.name == "Present") StyxSpawner.GetComponent<StyxRiver>().presents.Remove(gameObject.transform);
            else if(gameObject.name == "Piano") StyxSpawner.GetComponent<StyxRiver>().pianos.Remove(gameObject.transform);
        }
    }

    //Make it invisible when collided with Terrain
    void OnTriggerEnter(Collider col)
    {
        if(col.name == "Terrain")
        {
            gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.name == "Terrain")
        {
            gameObject.GetComponentInChildren<MeshRenderer>().enabled = true;
        }
    }
}