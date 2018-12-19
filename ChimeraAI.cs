using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChimeraAI : NPCAI
{
    public SnakeAttack meleeATK;
    public FireBreathAttack rangedATK;

    void Start()
    {
        //Overriding NPCAI default Variable Values
        curHealth = 250;
        maxHealth = 250;
        //NPCHeight = 2;
        NPCHeight = gameObject.GetComponent<CapsuleCollider>().height;
        inRot = 180;
        //xOffSet = 2;
        //zOffSet = 0;
        //moveSpeed = 0.06f;


        DamageArea = GameObject.Find("DamageArea");
        healthTextRotation.eulerAngles = new Vector3(0, transform.eulerAngles.y + inRot, 0);
        healthtext = Instantiate(healthTxtPrefab, new Vector3(transform.position.x, transform.position.y + NPCHeight + 0.5f, transform.position.z), healthTextRotation);
        healthtext.transform.SetParent(gameObject.transform);
        //tmesh = healthtext.GetComponent<TextMesh>();
        textmeshPro = healthtext.GetComponent<TextMeshPro>();
        if (autoHeal) InvokeRepeating("healthRegen", 0, 1);//Calls HealthRegen every second
        charType = getCharType();
    }

    void Update()
    {
        if (curHealth < 1) death();
        else if (curHealth > maxHealth) curHealth = maxHealth;

        //tmesh.text = (int)curHealth + " / " + maxHealth;
        if (displayHealth) textmeshPro.SetText((int)curHealth + " / " + maxHealth);

        if (!meleeAttacking) meleeATK.enabled = false;
        if(!rangedAttacking) rangedATK.enabled = false;

        if (Agrivated && (targets.Count != 0) && !inWater && Time.timeScale == 1)//IF two enemies die at the same time it crashes
        {
            if (meleeAttacking) meleeATK.enabled = true;
            if (rangedAttacking) rangedATK.enabled = true;

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
    }
    /*
    void OnTriggerEnter(Collider collision)//Adds object to vector if it is type
    {
        if (compatibilityChecker(collision.gameObject, charType))
        {
            if (collision.gameObject != TargetFromGameObject(targets, collision.gameObject).Obj)
            {
                Target temp = new global::ChimeraAI.Target(collision.gameObject, distance(collision.gameObject));
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
            if (targets[i] != null) targets[i].distance = distance(targets[i].Obj) - targets[i].radius;
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

    //Damage Functions
    public int damageCompatibilityChecker(bool Stay, GameObject test, int charType, bool canSwim = true)//Determines if gameObject does damage and returns dps
    {
        //Returns 0 if stay is true and the object is either a one time hit damage or not a damager at all.
        if (Stay)//Continous Damage things like Lava
        {
            if (test != null)//test is valid
            {
                if (test.gameObject.tag == "Lava")
                {
                    return 5;//5 DPS //Lava or fire
                }
                if (test.gameObject.tag == "Water" && !canSwim)
                {
                    return 5;//5 DPS //Water
                }
                try
                {
                    if ((charType == 1 || charType == 2 || charType == 3) && (test.gameObject.tag == "DamageArea") && ((test.gameObject.GetComponent("DamageArea") as DamageArea).root.tag == "Monster"))
                    {
                        return 5;
                    }
                    else return 0;//Not any of the Continous Damage Types
                }
                catch (NullReferenceException ex)
                {
                    Debug.Log("NPCAI: test null Reference Exception - Char Type: " + charType);
                    return 0;
                }
            }
            else return 0;
        }
        else//One Hit Damage
        {
            if (test.gameObject.tag == "Spikes") return 10;//10 damage one-time. //Spikes from Obstacle Course
            else if (test.gameObject.tag == "Damager" && test.gameObject.name != "ChimeraSnake(Clone)" && test.gameObject.name != "Chimera(Clone)Snake(Clone)") return 5;//5 damage one-time.  //Damagers are swords, arrows, spears, staffs, Monster Snake Head, etc
            else return 0;
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
}