using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PegasusAI : NPCAI
{
    public float speed = 50.0f;

	void Start ()
    {
        DamageArea = GameObject.Find("DamageArea");
        healthTextRotation.eulerAngles = new Vector3(0, transform.eulerAngles.y + inRot, 0);
        healthtext = Instantiate(healthTxtPrefab, new Vector3(transform.position.x, transform.position.y + NPCHeight + 0.5f, transform.position.z), healthTextRotation);
        healthtext.transform.SetParent(gameObject.transform);
        textmeshPro = healthtext.GetComponent<TextMeshPro>();

        if (autoHeal) InvokeRepeating("healthRegen", 0, 1);//Calls HealthRegen every second
        InvokeRepeating("turn", 0, UnityEngine.Random.Range(5,20));//Calls HealthRegen every second
        charType = getCharType();
    }
	
	void Update ()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(transform.up * speed * 100);

        if (curHealth < 1) death();
        else if (curHealth > maxHealth) curHealth = maxHealth;

        if (displayHealth) textmeshPro.SetText((int)curHealth + " / " + maxHealth);

        //if (!meleeAttacking) meleeATK.enabled = false;
        //if (!rangedAttacking) rangedATK.enabled = false;

        if (Agrivated && (targets.Count != 0) && !inWater && Time.timeScale == 1)//IF two enemies die at the same time it crashes
        {
            //if (meleeAttacking) meleeATK.enabled = true;
            //else meleeATK.enabled = false;
            //if (rangedAttacking) rangedATK.enabled = true;
            //else rangedATK.enabled = false;

            removeDeadTargets();
            sortedTargets = bubbleSort(targets);
            target = setTarget(sortedTargets);
            if (target != null && target.Obj != null)//IDK if both are necessary, but it works and I ain't fucking with it
            {
                lookAt(target);//Put in an invoke repeating somehow?
                moveForward(target, sightDistance, meleeDistance, rangeDistance, moveSpeed, hasRanged);
                setAllDistance();
            }
        }
    }

    void setAllDistance()//Updates distances for all objects in targets List
    {
        int length = targets.Count;
        for (int i = 0; i < length; i++)
        {
            if (targets[i] != null) targets[i].distance = distance(targets[i].Obj) - targets[i].radius;
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

    public void turn()
    {
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, UnityEngine.Random.Range(90, 270), transform.eulerAngles.z);
    }
    private void OnCollisionEnter(Collision collision)
    {
        turn();
    }
}