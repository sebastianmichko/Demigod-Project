using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    //[System.Serializable]
    public class Objective
    {
        public string name;
        public bool completed;
        public string description;
        public Objective(string name, string desc, bool done = false)
        {
            this.name = name;
            completed = done;
            description = desc;
        }
    }

    public Objective[] objectivesArray =
    {
        new Objective("foughtMinotaur", "Defeat the Minotaur before entering Camp Half-Blood"),
        new Objective("wakeUpScene", "Wake up and meet your saviors"),
        new Objective("spokeToLuke", "Meet your Camp Counselor"),
        new Objective("bathroomFight", "Scare off Ares Campers"),
        new Objective("wonCTF", "Win a game of Capture the Flag"),
        new Objective("beenClaimed", "Be Claimed by your Godly Parent"),
        new Objective("visitOracle", "Visit the Oracle in the Attic"),
        new Objective("leaveCamp", "Depart for your first Quest"),
        new Objective("beatMedusa", "Complete Mission 1"),
        new Objective("stLouisArch", "Complete Mission 2"),
        new Objective("hollywood", "Get into Hades"),
        new Objective("hades", "Complete Mission 3")
    };

    public Objective GetObjectiveFromString(string name)
    {
        for(int i = 0; i < objectivesArray.Length; ++i)
        {
            if (objectivesArray[i].name == name) return objectivesArray[i];
        }
        //print("There is no objective by the name " + name);
        return null;
    }

    void Start ()
    {
        Objective currentObjective = objectivesArray[0];
    }   
}