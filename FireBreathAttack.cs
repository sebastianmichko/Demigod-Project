using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBreathAttack : MonoBehaviour
{
    public AudioClip fireSound;//Petrify Swing
    public Transform Fire;//Prefab
    public Quaternion fireRot;
    public ChimeraAI ChimeraScript;
    public Transform flame;
    private string Name;
    public float rangedATKTime = 3.0f;
    public bool waiting = false;

    void Start()
    {
        Name = gameObject.name + "Flame(Clone)";
        ChimeraScript = gameObject.GetComponent<ChimeraAI>();
        rangedATKTime = gameObject.GetComponent<NPCAI>().rangedATKTime;
        //InvokeRepeating("updatePosition", 1, 2);//Calls Update Position every second
    }

    //Needs to take position, wait a second then spawn stone, do player has a chance to avoid getting damaged.
    void Update()
    {
        if(!waiting) flameThrower();
    }

    /*void updatePosition()//Runs every 2 seconds to allow player to dodge
    {
        GameObject target = ChimeraScript.target.Obj;
        pos = target.transform.position;
    }*/

    public void flameThrower()
    {
        if (!GameObject.Find(Name) && (ChimeraScript.target != null))
        {
            fireRot.eulerAngles = new Vector3(0, gameObject.transform.eulerAngles.y, 0);
            flame = Instantiate(Fire, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1.5f, gameObject.transform.position.z), fireRot);
            flame.name = Name;
            AudioSource.PlayClipAtPoint(fireSound, gameObject.transform.position, 3);// object's Z axis
            waiting = true;
            StartCoroutine(resumeAttack());
        }
    }

    public IEnumerator resumeAttack()
    {
        //print("ResumedVarCahnge");
        yield return new WaitForSeconds(rangedATKTime);
        waiting = false;
    }
}