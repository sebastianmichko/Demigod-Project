using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatArena : MonoBehaviour
{
    private GameObject Player;
    public bool fighting = false;
    GameObject Arena;

    //Combat Dude Variables
    public Transform combatDudePrefab;
    private Quaternion combatGuyRotation;
    public List<Transform> combatGuys = new List<Transform>();
    public int numCD;

    //Myrmeke Variables
    public Transform myrmekePrefab;
    private Quaternion myrmekeRotation;
    public List<Transform> myrmekes = new List<Transform>();
    public int numMyrmeke;

    //Minotaur Variables
    public Transform minotaurPrefab;
    private Quaternion minotaurRotation;
    public List<Transform> minotaurs = new List<Transform>();
    public int numMinotaur;

    //Medusa Variables
    public Transform medusaPrefab;
    private Quaternion medusaRotation;
    public List<Transform> medusas = new List<Transform>();
    public int numMedusas;

    //Chimera Variables
    public Transform chimeraPrefab;
    private Quaternion chimeraRotation;
    public List<Transform> chimeras = new List<Transform>();
    public int numChimeras;

    bool spawned = false;
    public GameObject border;

    void Start()
    {
        Player = GameObject.Find("ThirdPersonController");
        Arena = GameObject.Find("Combat Arena");

        //Combatant Rotations
        combatGuyRotation.eulerAngles = new Vector3(0, 0, 0);
        myrmekeRotation.eulerAngles = new Vector3(0, 0, 0);
        minotaurRotation.eulerAngles = new Vector3(0, 0, 0);
        medusaRotation.eulerAngles = new Vector3(0, 0, 0);
        chimeraRotation.eulerAngles = new Vector3(0, 0, 0);
    }

    void Update()
    {
        removeDeadEnemies(combatGuys);
        removeDeadEnemies(myrmekes);
        removeDeadEnemies(minotaurs);
        removeDeadEnemies(medusas);
        removeDeadEnemies(chimeras);
        if (fighting == false && Time.timeScale == 1)//Starts Game
        {
            Player.transform.position = new Vector3(Arena.transform.position.x - 10, 20, Arena.transform.position.z);
            Player.transform.rotation = Quaternion.Euler(0, 90, 0);

            numCD = GameObject.Find("Combat Arena Bleachers").GetComponent<CombatArenaMenu>().numCD;
            numMyrmeke = GameObject.Find("Combat Arena Bleachers").GetComponent<CombatArenaMenu>().numMyrmeke;
            numMinotaur = GameObject.Find("Combat Arena Bleachers").GetComponent<CombatArenaMenu>().numMinotaur;
            numMedusas = GameObject.Find("Combat Arena Bleachers").GetComponent<CombatArenaMenu>().numMedusas;
            numChimeras = GameObject.Find("Combat Arena Bleachers").GetComponent<CombatArenaMenu>().numChimeras;

            fighting = true;
            border.SetActive(true);
        }
        else if (Input.GetKeyUp("y") && fighting == true)//Exits Game
        {
            endGame();
        }
        //Spawning Computer Players
        if (fighting && !spawned)
        {
            Player.GetComponent<PlayerHealth>().curHealth = 100;
            //Combat Guys
            while (combatGuys.Count < numCD)
            {
                Transform temp = Instantiate(combatDudePrefab, new Vector3(Arena.transform.position.x + 7, 20.64f, Arena.transform.position.z + combatGuys.Count - 2), combatGuyRotation);
                temp.tag = "Monster";
                temp.name = "Demigod " + combatGuys.Count;
                combatGuys.Add(temp);
            }

            //Myrmekes
            while (myrmekes.Count < numMyrmeke)
            {
                Transform temp = Instantiate(myrmekePrefab, new Vector3(Arena.transform.position.x + (1.5f * myrmekes.Count) - 3, 20.64f, Arena.transform.position.z + 5), myrmekeRotation);
                temp.name = "Myrmeke " + myrmekes.Count;
                myrmekes.Add(temp);
            }

            //Minotaurs
            while (minotaurs.Count < numMinotaur)
            {
                Transform temp = Instantiate(minotaurPrefab, new Vector3(Arena.transform.position.x -3 + (3 * minotaurs.Count), 20.64f, Arena.transform.position.z - 5), minotaurRotation);
                temp.name = "Minotaur " + minotaurs.Count;
                minotaurs.Add(temp);
            }

            //Medusas
            while (medusas.Count < numMedusas)
            {
                Transform temp = Instantiate(medusaPrefab, new Vector3(Arena.transform.position.x + 2, 20.64f, Arena.transform.position.z), medusaRotation);
                temp.name = "Medusa " + medusas.Count;
                medusas.Add(temp);
            }

            //Chimeras
            while (chimeras.Count < numChimeras)
            {
                Transform temp = Instantiate(chimeraPrefab, new Vector3(Arena.transform.position.x - 2, 20.64f, Arena.transform.position.z), chimeraRotation);
                //temp.name = "Chimera " + chimeras.Count;
                chimeras.Add(temp);
            }
            spawned = true;
        }
        if(combatGuys.Count == 0 && myrmekes.Count == 0 && minotaurs.Count == 0 && medusas.Count == 0 && chimeras.Count == 0)//If all enemies have been killed - Relies on removeDeadEnemies() working
        {
            endGame();
        }
    }    
    
    void despawnAll()
    {
        foreach (var Transform in combatGuys)
        {
            Destroy(Transform.gameObject);
        }

        foreach (var Transform in myrmekes)
        {
            Destroy(Transform.gameObject);
        }

        foreach (var Transform in minotaurs)
        {
            Destroy(Transform.gameObject);
        }

        foreach (var Transform in medusas)
        {
            Destroy(Transform.gameObject);
        }

        foreach (var Transform in chimeras)
        {
            Destroy(Transform.gameObject);
        }
    }

    void removeDeadEnemies(List<Transform> list)//Can probably be put somewhere else
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (!list[i])  list.Remove(list[i]);
        }
    }
    void endGame()
    {
        fighting = false;
        gameObject.GetComponent<CombatArena>().enabled = false;
        spawned = false;
        border.SetActive(false);

        despawnAll();
        combatGuys.Clear();
        myrmekes.Clear();
        minotaurs.Clear();
        medusas.Clear();
        chimeras.Clear();
        Player.transform.position = new Vector3(Arena.transform.position.x - 10, 20, Arena.transform.position.z);
        Player.transform.rotation = Quaternion.Euler(0, 90, 0);
        Player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Player.GetComponent<PlayerHealth>().curHealth = 100;
    }
}