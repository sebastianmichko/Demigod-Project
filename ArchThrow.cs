using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchThrow : MonoBehaviour
{
    private GameObject Player; 

    public Transform explosionPrefab;
    public Quaternion expRotation;

    public Transform smokePrefab;

    public AudioClip sound; //Sword Swing soundClip

    public GameObject arch;
    public GameObject archHole;
    public GameObject platform;
    private bool shot = false;

    public int speed = 100;

    void Start()
    {
        Player = GameObject.Find("ThirdPersonController");
        arch = GameObject.Find("ARCH");
        archHole = GameObject.Find("ARCHHole");
        platform = GameObject.Find("ArchPlatform");
        //GetComponent<AudioSource>().Play();
    }

    void Update()
    {
        if (GameObject.Find("Chimera") == null && !shot)
        {
            Player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            shot = true;
            arch.SetActive(false);
            archHole.SetActive(true);
            platform.SetActive(false);
            Player.transform.position = new Vector3(245, 316, 327);
            Instantiate(explosionPrefab, Player.transform.position, expRotation);//Bang
            Instantiate(smokePrefab, Player.transform.position, expRotation);//Smoke
            AudioSource.PlayClipAtPoint(sound, Player.transform.position, 2);// object's Z axis
            Player.transform.eulerAngles = new Vector3(0, 180, 0);
            Player.GetComponent<Rigidbody>().AddForce(transform.right * speed * 1000);
            GetComponent<AudioSource>().Stop();
        }
    }
}