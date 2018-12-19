using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedusaSpawn : MonoBehaviour
{
    public GameObject entDoor;
    public GameObject extDoor;
    public GameObject exitWall;
    public GameObject exitTeleport;
    public Transform Medusa;
    private Quaternion medusaRotation;
    private Transform medusa;
    public bool activated = false;
    public bool won = false;
    public GameObject Player;
    public Transform curMedusa;

	void Start ()
    {
        Player = GameObject.Find("ThirdPersonController");
        AudioSource audio = GetComponent<AudioSource>();
    }
	
	void Update ()
    {
        if (curMedusa == null && activated)
        {
            extDoor.SetActive(false);
            exitWall.SetActive(false);
            won = true;
            Player.GetComponent<ProgressManager>().beatMedusa.completed = true;
            exitTeleport.SetActive(true);
            GetComponent<AudioSource>().Stop();
        }
    }

    //Triggers
    void OnTriggerEnter(Collider col)
    {
        if (activated == false)
        {
            if (col.gameObject.tag == "Demigod")
            {
                entDoor.SetActive(true);
                extDoor.SetActive(true);
                curMedusa = Instantiate(Medusa, new Vector3(transform.localPosition.x, 0, transform.localPosition.z), medusaRotation);
                activated = true;
                GetComponent<AudioSource>().Play();
            }
        }
    }
}