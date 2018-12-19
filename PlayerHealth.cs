using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    public float curHealth = 100.0f;
    public float maxHealth = 100.0f;
    //public GUIText healthtext;
    public HealthBarScript healthBarScript;
    public DeathMenu deathMenu;

    //public GameObject miniMap;
    //public GameObject miniMapCam;
    public GameObject Player;

    public bool dead = false;
    public bool autoHeal = true;
    public bool inWater = false;

    void healthRegen()
    {
        if (curHealth > 0)
        {
            if (curHealth < maxHealth)
            {
                curHealth++;
            }
        }
    }

    void Start()
    {
        if (autoHeal) InvokeRepeating("healthRegen", 0, 1);//Calls HealthRegen every second
        healthBarScript = gameObject.GetComponent<HealthBarScript>() as HealthBarScript;
        deathMenu = gameObject.GetComponent<DeathMenu>() as DeathMenu;
    }

    void death()
    {
        dead = true;
        Time.timeScale = 0;
        (gameObject.GetComponent("ThirdPersonCharacter") as MonoBehaviour).enabled = false;
        (gameObject.GetComponent("ThirdPersonUserControl") as MonoBehaviour).enabled = false;
        gameObject.transform.Find("EthanBody").gameObject.SetActive(false);
        healthBarScript.enabled = false;
        //if (miniMapCam != null)
        //{
            //miniMapCam.gameObject.SetActive(false);
            //miniMap.gameObject.SetActive(false);
        //}
        deathMenu.enabled = true;
    }
	
	void Update ()
    {
        if (curHealth <= 0 && dead == false)
        {
            death();
        }
        /*
        if (curHealth < 50)
        {

        }
        else
        {
            //hasPlayed = false;
            //GetComponent.<AudioSource>().volume = 0.0;
        }*/

        //healthtext.text = curHealth + " / " + maxHealth;

        if (curHealth < 0)
        {
            curHealth = 0;
        }

        if (curHealth > 100)
        {
            curHealth = 100;
        }

        if (Input.GetKeyDown("b"))
        {
            if (curHealth > 10)
            {
                curHealth -= 10;
            }
            else
            {
                curHealth = curHealth - curHealth;
            } 
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Water")
        {
            healthBarScript.foregroundTexture = healthBarScript.healingForeground;
            if (curHealth < 100)
            {
                curHealth += 1;
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Water")
        {
            healthBarScript.foregroundTexture = healthBarScript.regularForeground;
        }
    }

    public int damageCompatibilityChecker(bool Stay, GameObject test)//Determines if gameObject does damage and returns dps
    {
        //Returns 0 if stay is true and the object is either a one time hit damage or not a damager at all.
        if (Stay)//Continous Damage things like Lava
        {
            if (test != null)//test is valid
            {
                if (test.gameObject.tag == "Lava")
                {
                    return 10;//5 DPS //Lava or fire
                }
                try
                {
                    if ((test.gameObject.tag == "DamageArea") && ((test.gameObject.GetComponent("DamageArea") as DamageArea).root.tag == "Monster"))
                    {
                        return 10;//1 DPS Monster damaging Demigod
                    }
                    else return 0;//Not any of the Continous Damage Types
                }
                catch (NullReferenceException ex)
                {
                    Debug.Log("PlayerHealth: test nullReference Exception");
                    return 0;
                }

            }
            else return 0;
        }
        else//One Hit Damage
        {
            if (test.gameObject.tag == "Spikes") return 10;//10 damage one-time. //Spikes from Obstacle Course
            else if (test.gameObject.tag == "Damager" && test.gameObject.name != "ThirdPersonControllerSword(Clone)")
            {
                if (test.gameObject.name == "MedusaStone 1(Clone)") return 15;
                else if(test.gameObject.name == "FuryDamager(Clone)") return 15;
                else return 5;//5 damage one-time.  //Damagers are swords, arrows, spears, staffs, Monster Snake Head, etc
            }           
            else return 0;
        }
    }
    void OnParticleCollision(GameObject other)
    {
        curHealth -= damageCompatibilityChecker(true, other) * Time.deltaTime * 5;
    }
}