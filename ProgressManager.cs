using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    [System.Serializable]
    public class Objective
    {
        public bool completed;
        public string description;
        public Objective(bool done,string desc)
        {
            completed = done;
            description = desc;
        }
    }

    //Objectives
    public Objective foughtMinotaur = new Objective(false, "Defeat the Minotaur before entering Camp Half-Blood");
    public Objective wakeUpScene = new Objective(false, "Wake up and meet your saviors");
    public Objective spokeToLuke = new Objective(false, "Meet your Camp Counselor");
    public Objective bathroomFight = new Objective(false, "Scare off Ares Campers");
    public Objective wonCTF = new Objective(false, "Win a game of Capture the Flag");
    public Objective beenClaimed = new Objective(false, "Be Claimed by your Godly Parent");
    public Objective visitOracle = new Objective(false, "Visit the Oracle in the Attic");
    public Objective leaveCamp = new Objective(false, "Depart for your first Quest");
    public Objective beatMedusa = new Objective(false, "Complete Mission 1");
    public Objective stLouisArch = new Objective(false, "Complete Mission 2");

    public List<Objective> objectives = new List<Objective>();
    //public List<Objective> remainingObjectives = new List<Objective>();

    void Start ()
    {
        Objective currentObjective = foughtMinotaur;

        //All Objectives
        objectives.Add(foughtMinotaur);
        objectives.Add(wakeUpScene);
        objectives.Add(spokeToLuke);
        objectives.Add(bathroomFight);
        objectives.Add(wonCTF);
        objectives.Add(beenClaimed);
        objectives.Add(visitOracle);
        objectives.Add(leaveCamp);
        objectives.Add(beatMedusa);
        objectives.Add(stLouisArch);
    }
}