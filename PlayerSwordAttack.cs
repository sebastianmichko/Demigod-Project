using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordAttack : MonoBehaviour
{
    public Transform swordPrefab;
    private Quaternion swordRotation;
    private Transform sword;
    private int rot;
    public AudioClip sound; //Sword Swing soundClip
    private string Name;

    public bool meleeBlock = false;

    private PlayerHealth PlayerScript;


    void Start()
    {
        Name = gameObject.name + "Sword(Clone)";
        PlayerScript = gameObject.GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if (Time.timeScale == 1 && !meleeBlock)
        {
            if (Input.GetMouseButtonUp(0) && !PlayerScript.inWater )
            {
                if (GameObject.Find(Name) == null)//If no swords are instantiated then instantiate one.
                {
                    swordRotation.eulerAngles = new Vector3(90, transform.rotation.eulerAngles.y - 90, 0);
                    sword = Instantiate(swordPrefab, transform.position + (transform.right * 1.0f) + (transform.up) + (transform.forward * 0.75f), swordRotation);//good
                    sword.name = Name;
                    AudioSource.PlayClipAtPoint(sound, transform.position, 1);// object's Z axis
                }
            }
        }
    }
}