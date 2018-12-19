using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CTFCamperAI : NPCAI
{ 
    public SwordAttack meleeATK;

    //Capture the Flag Specific Variables
    GameObject ctfObj;
    CaptureTheFlag ctfScript;
    public bool team = true;//Red = true; Blue = false;
    public bool hasGuideon = false;

    void Start()
    {
        //Overriding NPCAI default Variable Values
        //NPCHeight = 2;
        NPCHeight = gameObject.GetComponent<CapsuleCollider>().height;
        inRot = 180;
        //xOffSet = 2;
        //zOffSet = 0;

        ctfObj = GameObject.Find("Capture the Flag");
        ctfScript = ctfObj.GetComponent<CaptureTheFlag>();
        DamageArea = GameObject.Find("DamageArea");
        if (team)
        {
            //healthTextRotation.eulerAngles = new Vector3(0, transform.eulerAngles.y + inRot, 0);
            healthTextRotation.eulerAngles = new Vector3(0, inRotAdd(transform.eulerAngles.y, inRot), 0);
        }
        else
        {
            //zOffSet = 2;
            //healthTextRotation.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180 + inRot, 0);
            healthTextRotation.eulerAngles = new Vector3(0, inRotAdd(transform.eulerAngles.y, inRot));
        }
        healthtext = Instantiate(healthTxtPrefab, new Vector3(transform.position.x, transform.position.y + NPCHeight + 0.5f, transform.position.z), healthTextRotation);
        healthtext.transform.SetParent(gameObject.transform);
        //tmesh = healthtext.GetComponent<TextMesh>();
        textmeshPro = healthtext.GetComponent<TextMeshPro>();
        if (autoHeal) InvokeRepeating("healthRegen", 0, 1);//Calls HealthRegen every second
        charType = getCharType();
    }

    void Update()
    {
        if (curHealth < 1 || !ctfScript.playingCTF) death();
        else if (curHealth > maxHealth) curHealth = maxHealth;

        //tmesh.text = (int)curHealth + " / " + maxHealth;
        if (displayHealth) textmeshPro.SetText((int)curHealth + " / " + maxHealth);

        if (!meleeAttacking) meleeATK.enabled = false;

        if (Agrivated && (targets.Count != 0) && !inWater && Time.timeScale == 1)//IF two enemies die at the same time it crashes
        {
            if (meleeAttacking) meleeATK.enabled = true;
            //else meleeATK.enabled = false;
            //if (rangedAttacking) rangedATK.enabled = true;
            //else rangedATK.enabled = false;

            removeDeadTargets();
            sortedTargets = bubbleSort(targets);
            target = setTarget(sortedTargets);
            if (target != null && target.Obj != null)
            {
                    lookAt(target);//Put in an invoke repeating somehow?
                    moveForward(target, sightDistance, meleeDistance, rangeDistance, moveSpeed, hasRanged);
                    setAllDistance();
            }
        }
        else//No Targets, Look for Flag - Walk Forward
        {
            //Attack Character
            if (team == false)//If Blue Team
            {
                if (hasGuideon)
                {
                    transform.eulerAngles = new Vector3(0, 90, 0);
                    transform.Translate(0, 0, moveSpeed);
                }
                else /*if(transform.position.x > 340)*/
                {
                    transform.Translate(0, 0, moveSpeed);
                    transform.eulerAngles = new Vector3(0, -90, 0);
                }
            }
            else//If Red Team
            {
                if (hasGuideon)
                {
                    transform.eulerAngles = new Vector3(0, -90, 0);
                    transform.Translate(0, 0, moveSpeed);
                }
                else /*if(transform.position.x < 600)*/
                {
                    transform.eulerAngles = new Vector3(0, 90, 0);
                    transform.Translate(0, 0, moveSpeed);
                }
            }
        }
    }
    /*
    void OnTriggerEnter(Collider collision)//Adds object to vector if it is type
    {
        if (compatibilityChecker(collision.gameObject, charType))
        {
            if (collision.gameObject != TargetFromGameObject(targets, collision.gameObject).Obj)
            {
                Target temp = new global::CTFCamperAI.Target(collision.gameObject, distance(collision.gameObject));
                temp.radius = collision.gameObject.GetComponent<CapsuleCollider>().radius;
                targets.Add(temp);
                Agrivated = true;
            }
        }
    }

    void OnTriggerExit(Collider collision)//Removes Object from vector
    {
        if (compatibilityChecker(collision.gameObject, charType))
        {
            if (targets.Count == 1)
            {
                meleeAttacking = false;
                rangedAttacking = false;
                Agrivated = false;
            }
            targets.Remove(TargetFromGameObject(targets, collision.gameObject));
        }
    }*/

    void setAllDistance()//Updates distances for all objects in targets List
    {
        int length = targets.Count;
        for (int i = 0; i < length; i++)
        {
            if (targets[i] != null)
                targets[i].distance = distance(targets[i].Obj) - targets[i].radius;
        }
    }
    /*
    void mRemove(Target tar)//Modified Remove - Removes target and deletes gameobject
    {
        //GameObject temp = tar.Obj;
        targets.Remove(tar);
        if (targets.Count > 1) target = sortedTargets[1];//Sets target to the second target
        else target = null;
        sortedTargets.Remove(tar);
        //Destroy(temp);//Causing Errors
    }*/

    void removeDeadTargets()//Can probably be put somewhere else
    {
        if (targets.Count == 0)
        {
            Agrivated = false;//May not need this.
            meleeAttacking = false;
            rangedAttacking = false;
        }
        for (int i = 0; i < targets.Count; i++)
        {
            if (!targets[i].Obj) /*targets.Remove(targets[i]);*/ mRemove(targets[i]);
            //if(!targets[i].Obj) mRemove(targets[i]);
            //print(targets[i].Obj + " Active: " + targets[i].Obj.activeSelf);
        }
    }

    Target setTarget(List<Target> sList)
    {
        if (sList.Count != 0)
        {
            Agrivated = true;
            return sList[0];
        }
        else
        {
            Agrivated = false;
            meleeAttacking = false;
            rangedAttacking = false;
            return null;
        }
    }

    //Health Functions
    void death()
    {
        Destroy(gameObject);
    }

    void healthRegen()
    {
        if (curHealth >= 1)
        {
            if (curHealth < maxHealth) curHealth++;
        }
    }

    void OnParticleCollision(GameObject other)
    {
        curHealth -= damageCompatibilityChecker(true, other, charType) * Time.deltaTime * 5;
    }
}