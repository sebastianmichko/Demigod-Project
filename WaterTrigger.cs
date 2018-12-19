using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTrigger : MonoBehaviour
{
    //Various Script Types
    public AntAI AntScript;
    //Swim

    public DemigodAI DemigodScript;
    //Swim

    public MinotaurAI MinotaurScript;
    //Swim

    public MedusaAI MedusaScript;
    //Swim

    public PlayerHealth PlayerScript;
    public Swimming PlayerSwim;

    public ChimeraAI ChimeraScript;
    //Swim

    private int whichScript = 0;//0=null, 1=ANT, 2=Demigod, 3=Minotaur, 4=Medusa, 5=player, 6 = Chimera...

    public AudioSource underwaterAudio;

    private void Start()
    {
        AudioSource underwaterAudio = GetComponent<AudioSource>();
    }

    //Enter Water
    void OnTriggerEnter(Collider collision)
    {
        if(collision.GetComponent<DamageArea>())//When the Character's Damage Area Trigger Touches the Water Swim Trigger
        {
            if (collision.GetComponentInParent<AntAI>())
            {
                AntScript = collision.GetComponentInParent<AntAI>();
                AntScript.inWater = true;
                whichScript = 1;
            }
            else if (collision.GetComponentInParent<DemigodAI>())
            {
                DemigodScript = collision.GetComponentInParent<DemigodAI>();
                DemigodScript.inWater = true;
                whichScript = 2;
            }
            else if (collision.GetComponentInParent<MinotaurAI>())
            {
                MinotaurScript = collision.GetComponentInParent<MinotaurAI>();
                MinotaurScript.inWater = true;
                whichScript = 3;
            }
            else if (collision.GetComponentInParent<MedusaAI>())
            {
                MedusaScript = collision.GetComponentInParent<MedusaAI>();
                MedusaScript.inWater = true;
                whichScript = 4;
            }
            else if (collision.GetComponentInParent<PlayerHealth>())//1
            {
                PlayerScript = collision.GetComponentInParent<PlayerHealth>();
                PlayerSwim = collision.GetComponentInParent<Swimming>();
                PlayerScript.inWater = true;
                PlayerSwim.inWater = true;
                whichScript = 5;
            }
            else if (collision.GetComponentInParent<ChimeraAI>())
            {
                ChimeraScript = collision.GetComponentInParent<ChimeraAI>();
                ChimeraScript.inWater = true;
                whichScript = 6;
            }

            if (whichScript == 5)//Why is this here??????????????????????? Why not just put these statements in the *1 above? Same below in Trigger Exit
            {
                PlayerSwim.whatToDo(true);
                underwaterAudio.Play();
            }
        }   
    }

    //Leave Water
    void OnTriggerExit(Collider collision)
    {
        if (collision.GetComponent<DamageArea>())//When the Character's Damage Area Trigger Touches the Water Swim Trigger
        {
            if (collision.GetComponentInParent<AntAI>())
            {
                AntScript.inWater = false;
                whichScript = 1;
            }
            else if (collision.GetComponentInParent<DemigodAI>())
            {
                DemigodScript.inWater = false;
                whichScript = 2;
            }
            else if (collision.GetComponentInParent<MinotaurAI>())
            {
                MinotaurScript.inWater = false;
                whichScript = 3;
            }
            else if (collision.GetComponentInParent<MedusaAI>())
            {
                MedusaScript.inWater = false;
                whichScript = 4;
            }
            else if (collision.GetComponentInParent<PlayerHealth>())
            {
                PlayerScript.inWater = false;
                PlayerSwim.inWater = false;
                whichScript = 5;
            }
            else if (collision.GetComponentInParent<ChimeraAI>())
            {
                ChimeraScript.inWater = false;
                whichScript = 6;
            }

            if (whichScript == 5)
            {
                PlayerSwim.whatToDo(false);
                underwaterAudio.Stop();
            }
        }
    }
}