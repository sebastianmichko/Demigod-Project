using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    public GameObject root;
    //Various Script Types
    //public AntAI AntScript;
    //public DemigodAI DemigodScript;
    //public MinotaurAI MinotaurScript;
    //public MedusaAI MedusaScript;
    public PlayerHealth PlayerScript;
    //public ChimeraAI ChimeraScript;
    //private CTFCamperAI CTFScript;

    public NPCAI AIScript;

    //private int whichScript = 0;//0=null, 1=ANT, 2=Demigod, 3=Minotaur, 4=Medusa, 5=player, 6=Chimera ...
    private bool onPlayer;


    void Start ()
    {
        root = gameObject.transform.parent.gameObject;
        /*if (root.GetComponent<AntAI>())
        {
            AntScript = root.GetComponent<AntAI>();
            whichScript = 1;
        } 
        else if (root.GetComponent<DemigodAI>())
        {
            DemigodScript = root.GetComponent<DemigodAI>();
            whichScript = 2;
        }
        else if (root.GetComponent<MinotaurAI>())
        {
            MinotaurScript = root.GetComponent<MinotaurAI>();
            whichScript = 3;
        }
        else if (root.GetComponent<MedusaAI>())
        {
            MedusaScript = root.GetComponent<MedusaAI>();
            whichScript = 4;
        }
        else if (root.GetComponent<PlayerHealth>())
        {
            PlayerScript = root.GetComponent<PlayerHealth>();
            whichScript = 5;
        }
        else if (root.GetComponent<ChimeraAI>())
        {
            ChimeraScript = root.GetComponent<ChimeraAI>();
            whichScript = 6;
        }
        else if (root.GetComponent<CTFCamperAI>())
        {
            CTFScript = root.GetComponent<CTFCamperAI>();
            whichScript = 7;
        }*/
        if (root.GetComponent<NPCAI>())
        {
            AIScript = root.GetComponent<NPCAI>();//Means this script is attached to an NPC instead of the player
            onPlayer = false;
        }

        else if (root.GetComponent<PlayerHealth>())
        {
            PlayerScript = root.GetComponent<PlayerHealth>();//This script is attached to a player
            onPlayer = true;
        }
    }

    void OnTriggerEnter(Collider collision)
    {/*
        if (whichScript == 1) AntScript.curHealth -= AntScript.damageCompatibilityChecker(false, collision.gameObject, AntScript.charType);
        else if (whichScript == 2) DemigodScript.curHealth -= DemigodScript.damageCompatibilityChecker(false, collision.gameObject, DemigodScript.charType);
        else if (whichScript == 3) MinotaurScript.curHealth -= MinotaurScript.damageCompatibilityChecker(false, collision.gameObject, MinotaurScript.charType);
        else if (whichScript == 4) MedusaScript.curHealth -= MedusaScript.damageCompatibilityChecker(false, collision.gameObject, MedusaScript.charType);
        else if (whichScript == 5) PlayerScript.curHealth -= PlayerScript.damageCompatibilityChecker(false, collision.gameObject);
        else if (whichScript == 6) ChimeraScript.curHealth -= ChimeraScript.damageCompatibilityChecker(false, collision.gameObject, ChimeraScript.charType);
        else if (whichScript == 7) CTFScript.curHealth -= CTFScript.damageCompatibilityChecker(false, collision.gameObject, CTFScript.charType);*/
        if(!onPlayer) AIScript.curHealth -= AIScript.damageCompatibilityChecker(false, collision.gameObject, AIScript.charType);
        else PlayerScript.curHealth -= PlayerScript.damageCompatibilityChecker(false, collision.gameObject);
    }

    void OnTriggerStay(Collider collision)
    {/*
        if (whichScript == 1) AntScript.curHealth -= AntScript.damageCompatibilityChecker(true, collision.gameObject, AntScript.charType, AntScript.canSwim) * Time.deltaTime * 1;
        else if (whichScript == 2) DemigodScript.curHealth -= DemigodScript.damageCompatibilityChecker(true, collision.gameObject, DemigodScript.charType, DemigodScript.canSwim) * Time.deltaTime * 1;
        else if (whichScript == 3) MinotaurScript.curHealth -= MinotaurScript.damageCompatibilityChecker(true, collision.gameObject, MinotaurScript.charType, MinotaurScript.canSwim) * Time.deltaTime * 1;
        else if (whichScript == 4) MedusaScript.curHealth -= MedusaScript.damageCompatibilityChecker(true, collision.gameObject, MedusaScript.charType, MedusaScript.canSwim) * Time.deltaTime * 1;
        else if (whichScript == 5) PlayerScript.curHealth -= PlayerScript.damageCompatibilityChecker(true, collision.gameObject) * Time.deltaTime * 1;
        else if (whichScript == 6) ChimeraScript.curHealth -= ChimeraScript.damageCompatibilityChecker(true, collision.gameObject, ChimeraScript.charType, ChimeraScript.canSwim) * Time.deltaTime * 1;
        else if (whichScript == 7) CTFScript.curHealth -= CTFScript.damageCompatibilityChecker(true, collision.gameObject, CTFScript.charType, CTFScript.canSwim) * Time.deltaTime * 1;*/
        if(!onPlayer) AIScript.curHealth -= AIScript.damageCompatibilityChecker(true, collision.gameObject, AIScript.charType, AIScript.canSwim) * Time.deltaTime * 1;
        else PlayerScript.curHealth -= PlayerScript.damageCompatibilityChecker(true, collision.gameObject) * Time.deltaTime * 1;
    }
}