using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCAI : MonoBehaviour
{
    //Character Type
    public int charType = 1;//1 - Reg Halfblood, 2 - Red HalfBlood, 3 - Blue Halfblood, 4 - Monster //Will Attack: (Halfblood(reg, red, or blue) - Monster), (HalfBlood(Red) - HalfBlood(Blue))

    //Health
    public float curHealth = 100;
    public int maxHealth = 100;
    public bool autoHeal = false;
    public bool displayHealth = false;
    public GameObject DamageArea;

    //Health Text
    protected GameObject healthtext;
    public GameObject healthTxtPrefab;
    protected Quaternion healthTextRotation;
    public float NPCHeight = 2.0f;
    public int inRot = 0;
    public float xOffSet = 0;
    public float zOffSet = 0;
    protected TextMesh tmesh;
    protected TextMeshPro textmeshPro;

    //Movement
    public bool canSwim = false;
    public double swimSpeed = 1.5;
    public float moveSpeed = 0.05f;
    public float damping = 6.0f;//Some shit for look at function
    public bool inWater = false;

    //Combat:
    public double sightDistance = 15;
    public bool Agrivated = false;//Any enemy is within Sight Distance - WIll move towards enemy
    public bool hasRanged = false;//Character has a ranged attack
    public double rangeDistance = 10;//Distance at which character will start shooting arrows, shooting poison,, breathing fire, magic, turning to stone, etc //Will give up on ranged attacks at 1/2 Range DIstance
    public double meleeDistance = 2;//Distance at which character will start swinging sword, striking, etc
    public bool meleeAttacking = false;
    public bool rangedAttacking = false;
    public float meleeATKTime = 1.0f;
    public float rangedATKTime = 1.0f;

    public List<Target> targets = new List<Target>();
    public List<Target> sortedTargets;
    public Target target = new global::NPCAI.Target();

    //Targets:
    [System.Serializable]
    public class Target
    {
        public GameObject Obj;
        public double distance;
        public double radius;
        //public int type;

        public Target(GameObject ob, double ds/*, int tp*/)
        {
            Obj = ob;
            distance = ds;
            radius = 0;
            //type = tp;
        }
        public Target() { }
    }
    /*
    void Update()
    {
        if (displayHealth) textmeshPro.SetText( (int)curHealth + " / " + maxHealth);
    }*/

    public double distance(GameObject target)//Finds distance from this character to the target
    {
        //if (target != null) return Vector3.Distance(target.transform.position, transform.position); ;
        if (target != null) return Vector3.Distance(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z), transform.position);//No longer takes y into account so things stop spinning ontop of eachother
        else return 999.0;
    }

    public int getCharType()//1 - Reg Halfblood, 2 - Red HalfBlood, 3 - Blue Halfblood, 4 - Monster //Will Attack: (Halfblood(reg, red, or blue) - Monster), (HalfBlood(Red) - HalfBlood(Blue))
    {
        int ctype = 1;
        if (gameObject.tag == "Demigod") ctype = 1;
        else if (gameObject.tag == "RedTeam") ctype = 2;
        else if (gameObject.tag == "BlueTeam") ctype = 3;
        else if (gameObject.tag == "Monster") ctype = 4;

        return ctype;
    }

   public List<Target> bubbleSort(List<Target> list)
    {
        int length = list.Count;

        if (length != 0)
        {
            Target temp = list[0];

            for (int i = 0; i < length; i++)
            {
                for (int j = i + 1; j < length; j++)
                {
                    if (list[i].distance > list[j].distance)
                    {
                        temp = list[i];

                        list[i] = list[j];

                        list[j] = temp;
                    }
                }
            }
        }
        return list;
    }

    //Movement Functions:
    public void lookAt(Target tar)
    {
        Vector3 targetPostition = new Vector3(tar.Obj.transform.position.x, transform.position.y, tar.Obj.transform.position.z);
        this.transform.LookAt(targetPostition);
    }

    public Target TargetFromGameObject(List<Target> list, GameObject obj)//Returns a Target Object from the targets list based off of a gameobject;
    {
        Target temp = new global::NPCAI.Target();//Tried "= null" before. It doesn't work properly
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].Obj == obj)
            {
                temp = list[i];
            }
        }
        return temp;
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
                if (test.gameObject.name == "Water Swim Trigger" && !canSwim)
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
                    Debug.Log("NPCAI: test null Reference Exception - Char Type: " + charType );
                    return 0;
                }
            }
            else return 0;
        }
        else//One Hit Damage
        {
            if (test.gameObject.tag == "Spikes") return 10;//10 damage one-time. //Spikes from Obstacle Course
            else if (test.gameObject.tag == "Damager") return 5;//5 damage one-time.  //Damagers are swords, arrows, spears, staffs, Monster Snake Head, etc
            else return 0;
        }
    }

    public bool compatibilityChecker(GameObject test, int cType)//Determines if target will be attacked
    {
        bool temp = false;

        switch (cType)////1 - Reg Halfblood, 2 - Red HalfBlood, 3 - Blue Halfblood, 4 - Monster
        {
            case 1://If this code is attached to a reg. Demigod
                if (test.tag == "Monster") temp = true;
                break;
            case 2://If this code is attached to a Red Team Demigod
                if ((test.tag == "Monster") || (test.tag == "BlueTeam")) temp = true;
                break;
            case 3://If this code is attached to a Blue Team Demigod
                if ((test.tag == "Monster") || (test.tag == "RedTeam")) temp = true;
                break;
            case 4://If this code is attached to a Monster
                if (test.tag == "Demigod" || test.tag == "RedTeam" || test.tag == "BlueTeam") temp = true;
                break;
            default:
                temp = false;
                break;
        }
        return temp;
    }
    
    public void moveForward(Target tar, double sightDistance, double meleeDistance, double rangeDistance, float moveSpeed, bool hasRanged)//If has ranged, then if at greater than 0.5*sightDistance attack with ranged.  IF less than 0.5*sightDistance then advance forward melee
    {
        if ((tar.distance <= sightDistance) && (tar.distance > meleeDistance) && ((tar.distance > rangeDistance) || !hasRanged))//Too Far Away to Melee or range, advance
        {
            transform.Translate(0, 0, moveSpeed);
            meleeAttacking = false;
            rangedAttacking = false;
        }
        else if (hasRanged && (tar.distance <= rangeDistance) && (tar.distance >= rangeDistance - 1))//Ranged Attack (if possible)
        { 
            meleeAttacking = false;
            rangedAttacking = true;
        }
        else if (hasRanged && (tar.distance <= (rangeDistance - 1)) && (tar.distance >= 0.5 * rangeDistance))//Move Backward into Range Distance
        {
            transform.Translate(0, 0, -moveSpeed);
            meleeAttacking = false;
            rangedAttacking = false;
        }
        else if (hasRanged && (tar.distance >= meleeDistance) && (tar.distance < 0.5 * rangeDistance))   //Move Forward into melee distance
        {
            transform.Translate(0, 0, moveSpeed);
            meleeAttacking = false;
            rangedAttacking = false;
        }
        else if (tar.distance < (meleeDistance - 1))//Too Close to Melee, backup
        {
            transform.Translate(0, 0, -moveSpeed);
            meleeAttacking = false;
            rangedAttacking = false;
        }
        else if ((tar.distance < meleeDistance) && (tar.distance >= meleeDistance - 1))//Melee Attack
        {
            if(!inWater)
            {
                meleeAttacking = true;
                rangedAttacking = false;
            }
        }
        //print((meleeDistance - 1.2f) + " < " + tar.distance + " < " + meleeDistance);
    }

    /*//Test One
    public void moveForward(Target tar, double sightDistance, double meleeDistance, double rangeDistance, float moveSpeed, bool hasRanged)//If has ranged, then if at greater than 0.5*sightDistance attack with ranged.  IF less than 0.5*sightDistance then advance forward melee
    {
        double disMinRad = tar.distance - tar.radius;
        if ((disMinRad <= sightDistance) && (disMinRad > meleeDistance) && ((disMinRad > rangeDistance) || !hasRanged))//Too Far Away to Melee or range, advance
        {
            transform.Translate(0, 0, moveSpeed);
            meleeAttacking = false;
            rangedAttacking = false;
        }
        else if (hasRanged && (disMinRad <= rangeDistance) && (disMinRad >= rangeDistance - 1))//Ranged Attack (if possible)
        {
            meleeAttacking = false;
            rangedAttacking = true;
        }
        else if (hasRanged && (disMinRad <= (rangeDistance - 1)) && (disMinRad >= 0.5 * rangeDistance))//Move Backward into Range Distance
        {
            transform.Translate(0, 0, -moveSpeed);
            meleeAttacking = false;
            rangedAttacking = false;
        }
        else if (hasRanged && (disMinRad >= meleeDistance) && (disMinRad < 0.5 * rangeDistance))   //Move Forward into melee distance
        {
            transform.Translate(0, 0, moveSpeed);
            meleeAttacking = false;
            rangedAttacking = false;
        }
        else if (disMinRad < (meleeDistance - 1))//Too Close to Melee, backup
        {
            transform.Translate(0, 0, -moveSpeed);
            meleeAttacking = false;
            rangedAttacking = false;
        }
        else if ((meleeDistance - 1 <= disMinRad) && (disMinRad < meleeDistance))//Melee Attack
        {
            meleeAttacking = true;
            rangedAttacking = false;
        }
        print((meleeDistance - 1) + " < " + disMinRad + " < " + (meleeDistance));
    }*/

    public void mRemove(Target tar)//Modified Remove - Removes target and deletes gameobject
    {
        targets.Remove(tar);
        if (targets.Count > 1) target = sortedTargets[1];//Sets target to the second target
        else target = null;
        sortedTargets.Remove(tar);
    }

    public Target setTarget(List<Target> sList, bool Agrivated)
    {
        if (sList.Count != 0)
        {
            Agrivated = true;
            return sList[0];
        }
        else
        {
            Agrivated = false;
            return null;
        }
    }

    void healthRegen(float curHealth, int maxHealth)
    {
        if (curHealth > 0)
        {
            if (curHealth < maxHealth) curHealth++;
        }
    }

    public float inRotAdd(float rotation, int inRot)
    {
        if (rotation >= 0) return rotation + inRot;
        else if (rotation < 0) return rotation - inRot;
        else return 0.0f;
    }
}