using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class MedusaAI : NPCAI
{
    public StaffAttack meleeATK;
    public PetrifyAttack rangedATK;

    void Start()
    {
        //Overriding NPCAI default Variable Values
        curHealth = 250;
        maxHealth = 250;
        //NPCHeight = 3;
        NPCHeight = gameObject.GetComponent<CapsuleCollider>().height;
        inRot = 180;
        //moveSpeed = 0.03f;
        hasRanged = true;

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
        if (!rangedAttacking) rangedATK.enabled = false;

        if (Agrivated && (targets.Count != 0) && !inWater && Time.timeScale == 1)//IF two enemies die at the same time it crashes
        {
            if(meleeAttacking) meleeATK.enabled = true;
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
                Target temp = new global::MedusaAI.Target(collision.gameObject, distance(collision.gameObject));
                temp.radius = collision.gameObject.GetComponent<CapsuleCollider>().radius;
                targets.Add(temp);
                Agrivated = true;
            }
        }
    }*/
    /*
    void OnTriggerExit(Collider collision)//Removes Object from vector
    {
        if (compatibilityChecker(collision.gameObject, charType))
        {
            mRemove(TargetFromGameObject(targets, collision.gameObject));
            if (targets.Count == 0)
            {
                meleeAttacking = false;
                rangedAttacking = false;
                Agrivated = false;
            }
            print("Lost " + collision.name);
        }
    }*/

   /* void mRemove(Target tar)//Modified Remove - Removes target and deletes gameobject
    {
        targets.Remove(tar);
        if (targets.Count > 1) target = sortedTargets[1];//Sets target to the second target
        else target = null;
        sortedTargets.Remove(tar);
    }*/

    void setAllDistance()//Updates distances for all objects in targets List
    {
        int length = targets.Count;
        for (int i = 0; i < length; i++)
        {
            if (targets[i] != null)
            {
                targets[i].distance = distance(targets[i].Obj) - targets[i].radius;
            }
        }
    }

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