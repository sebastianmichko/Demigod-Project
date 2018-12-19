using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using Narrate;

public class Cheats : MonoBehaviour
{
    public bool cheatsEnabled = false;

    //Spawn Variables
    public float maxDistance = 10;
    public float minDistance = 2;
    
    public Transform myrmekePrefab;
    public List<Transform> myrmekes = new List<Transform>();

    public Transform minotaurPrefab;
    public List<Transform> minotaurs = new List<Transform>();

    public Transform combatDudePrefab;
    public List<Transform> combatGuys = new List<Transform>();

    public Transform medusaPrefab;
    public List<Transform> medusas = new List<Transform>();

    public Transform chimeraPrefab;
    public List<Transform> chimeras = new List<Transform>();

    public Transform furyPrefab;
    public List<Transform> furys = new List<Transform>();

    private Quaternion spawnRotation;

    public bool superRunEnabled;

    private void Start()
    {
        spawnRotation.eulerAngles = new Vector3(0, 0, 0);
    }

    void Update ()
    {
        if (Input.GetKeyUp("`"))
        {
            if (!cheatsEnabled) cheatsEnabled = true;
            else
            {
                cheatsEnabled = false;
                fly(true);
                superRun(true);
                disableSuperDamage();
            }
        }

        if (cheatsEnabled)
        {
            if (Input.GetKeyUp("1")) increaseHealth();//Increase Health by 10
            if (Input.GetKeyUp("2")) fly();
            if (Input.GetKeyUp("3")) superRun();
            if (Input.GetKeyUp("4")) enableSuperDamage();
            if (Input.GetKeyUp("5")) spawn(1);//Myremeke
            if (Input.GetKeyUp("6")) spawn(2);//Minotaur
            if (Input.GetKeyUp("7")) spawn(3);//Demigod
            if (Input.GetKeyUp("8")) spawn(4);//Medusa
            if (Input.GetKeyUp("9")) spawn(5);//Chimera
            if (Input.GetKeyUp("0")) spawn(6);//Fury		
        }
	}

    public void increaseHealth()
    {
        if (transform.GetComponent<PlayerHealth>().curHealth <= (transform.GetComponent<PlayerHealth>().maxHealth - 10))
        {
            transform.GetComponent<PlayerHealth>().curHealth += 10;
        }
        else if(transform.GetComponent<PlayerHealth>().curHealth > (transform.GetComponent<PlayerHealth>().maxHealth - 10))
        {
            transform.GetComponent<PlayerHealth>().curHealth = transform.GetComponent<PlayerHealth>().maxHealth;
        }
    }

    void fly(bool disable = false)
    {

    }

    void superRun(bool disable = false)
    {
        if(superRunEnabled || disable)
        {
            GetComponent<ThirdPersonCharacter>().m_MoveSpeedMultiplier = 1;
            superRunEnabled = false;
        }
        else
        {
            GetComponent<ThirdPersonCharacter>().m_MoveSpeedMultiplier = 5;
            superRunEnabled = true;
        }
    }

    void enableSuperDamage()//Need to make a seperate Player's Damager Item
    {
        
    }

    void disableSuperDamage()
    {

    }

    void spawn(int NPCType)
    {
        switch (NPCType)
        {
            case 1:
                Transform temp1 = Instantiate(myrmekePrefab, new Vector3(transform.position.x + areaRandom(), transform.position.y, transform.position.z + areaRandom()), spawnRotation );
                temp1.name = "Myrmeke " + myrmekes.Count;
                myrmekes.Add(temp1);
                break;

            case 2:
                Transform temp2 = Instantiate(minotaurPrefab, new Vector3(transform.position.x + areaRandom(), transform.position.y, transform.position.z + areaRandom()), spawnRotation);
                temp2.name = "Minotaur " + minotaurs.Count;
                minotaurs.Add(temp2);
                break;

            case 3:
                Transform temp3 = Instantiate(combatDudePrefab, new Vector3(transform.position.x + areaRandom(), transform.position.y, transform.position.z + areaRandom()), spawnRotation);
                //temp.tag = "Monster";
                temp3.name = "Demigod " + combatGuys.Count;
                combatGuys.Add(temp3);
                break;

            case 4:
                Transform temp4 = Instantiate(medusaPrefab, new Vector3(transform.position.x + areaRandom(), transform.position.y, transform.position.z + areaRandom()), spawnRotation);
                temp4.name = "Medusa " + medusas.Count;
                medusas.Add(temp4);
                break;

            case 5:
                Transform temp5 = Instantiate(chimeraPrefab, new Vector3(transform.position.x + areaRandom(), transform.position.y, transform.position.z + areaRandom()), spawnRotation);
                temp5.name = "Chimera " + chimeras.Count;
                chimeras.Add(temp5);
                break;

            case 6:
                Transform temp6 = Instantiate(furyPrefab, new Vector3(transform.position.x + areaRandom(), transform.position.y, transform.position.z + areaRandom()), spawnRotation);
                temp6.name = "Fury " + furys.Count;
                furys.Add(temp6);
                break;
        }
    }

    public float areaRandom()
    {
        if(Random.value >= 0.5f)
        {
            return -1 * Random.Range(minDistance, maxDistance);
        }
        else
        {
            return Random.Range(minDistance, maxDistance);
        }
    }
}