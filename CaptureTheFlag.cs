using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureTheFlag : MonoBehaviour {

    private GameObject Player; 
    public bool playingCTF = false;
    public Transform FlagPrefab;
    private Quaternion flagRotation;
    private float zFL;
    private float zBL;
    private float bLL = 610;
    private float bRL = 550;
    private float rLL = 380;
    private float rRL = 335;

    private Transform redFlag; 
    private Transform blueFlag;

    public Transform CTFCamperPrefab;
    public Transform monSpawnPrefab;
    private Quaternion redCamperRotation;
    private Quaternion blueCamperRotation;
    private Quaternion monSpawnRotation;
    public int maxCamper = 15;//Team Size - Player is included in team size

    public int maxNumMonsterSpawns = 0;//# of random Ambient Monster Spawns

    public List<Transform> monSpawns = new List<Transform>();
    public List<Transform> redTeam = new List<Transform>();
    public List<Transform> blueTeam = new List<Transform>();
    public GameObject blueSide;
    public GameObject redSide;
    public GameObject border;

    public Material RedDemigod;
    public Material BlueDemigod;



    void Start ()
    {
        Player = GameObject.Find("ThirdPersonController");
        zFL = (transform.position.z + 60);
        zBL = (transform.position.z - 60);
        flagRotation.eulerAngles = new Vector3(0, 0, 0);
        redCamperRotation.eulerAngles = new Vector3(0, 0, 0);//90
        blueCamperRotation.eulerAngles = new Vector3(0, 0, 0);//-90
        monSpawnRotation.eulerAngles = new Vector3(0, 0, 0);
    }
	
	void Update ()
    {
        //if(Input.GetKeyUp("e") && playingCTF == false && close && Time.timeScale == 1)//Starts Game
        if (playingCTF == false && Time.timeScale == 1)//Starts Game
        {
            if (GameObject.Find("CTF Guy").GetComponent<CaptureTheFlagMenu>().side == true)//Player is Red
            {
                Player.transform.position = new Vector3(Random.Range(rRL, rLL), 25, Random.Range(zFL, zBL));
                Player.transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            else//Player is Blue
            {
                Player.transform.position = new Vector3(Random.Range(bRL, bLL), 25, Random.Range(zFL, zBL));
                Player.transform.rotation = Quaternion.Euler(0, 270, 0);
            }

            maxCamper = GameObject.Find("CTF Guy").GetComponent<CaptureTheFlagMenu>().numPerTeam;
            maxNumMonsterSpawns = GameObject.Find("CTF Guy").GetComponent<CaptureTheFlagMenu>().numMonsterSpawns;
            playingCTF = true;
            border.SetActive(true);
        }
        else if (Input.GetKeyUp("y") && playingCTF == true)//Exits Game
        {
            playingCTF = false;
            (gameObject.GetComponent("CaptureTheFlag") as MonoBehaviour).enabled = false;
            border.SetActive(false);
            //Destroys the Flags when game done
            Destroy(redFlag.gameObject);
            Destroy(blueFlag.gameObject);
            blueTeam.Clear();
            redTeam.Clear();
            Player.transform.position = new Vector3(330, 20, 220);
            Player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        //Flags
        if (playingCTF && blueFlag == null && redFlag == null)//Need only two flags
        {
            blueFlag = Instantiate(FlagPrefab, new Vector3(Random.Range(bRL, bLL), 25, Random.Range(zFL, zBL)), flagRotation);//Blue Flag
            blueFlag.GetComponent<Flag>().redTeam = false;
            blueFlag.name = "BlueFlag";
            blueFlag.GetComponent<Flag>().banner.GetComponent<Renderer>().material.color = Color.blue;

            redFlag = Instantiate(FlagPrefab, new Vector3(Random.Range(rRL, rLL), 25, Random.Range(zFL, zBL)), flagRotation);//Red Flag
            redFlag.GetComponent<Flag>().redTeam = true;
            redFlag.name = "RedFlag";
            redFlag.GetComponent<Flag>().banner.GetComponent<Renderer>().material.color = Color.red;
        }

        //Computer Players
        if (playingCTF)
        {
            //Player
            if (GameObject.Find("CTF Guy").GetComponent<CaptureTheFlagMenu>().side == true)
            {
                if (!redTeam.Contains(Player.transform))
                {
                    redTeam.Add(Player.transform);
                    Player.tag = "RedTeam";
                }

            }
            else
            {
                if (!blueTeam.Contains(Player.transform))
                {
                    blueTeam.Add(Player.transform);
                    Player.tag = "BlueTeam";
                }

            }

            //Red Team NPCS
            if (redTeam.Count < maxCamper)//-1 due to the Player always being on the red team.(For Now)
            {
                Transform temp = Instantiate(CTFCamperPrefab, new Vector3(Random.Range(rRL, rLL), 25, Random.Range(zFL, zBL)), redCamperRotation);
                temp.name = "RedTeamDemigod" + redTeam.Count;
                temp.GetComponent<CTFCamperAI>().team = true;
                temp.GetComponent<CTFCamperAI>().charType = 2;
                temp.tag = "RedTeam";
                temp.GetComponentInChildren<Renderer>().material = RedDemigod;
                redTeam.Add(temp);
            }

            //Blue Team NPCs
            if (blueTeam.Count < maxCamper)
            {
                Transform temp = Instantiate(CTFCamperPrefab, new Vector3(Random.Range(bRL, bLL), 25, Random.Range(zFL, zBL)), blueCamperRotation);
                temp.name = "BlueTeamDemigod" + blueTeam.Count;
                temp.GetComponent<CTFCamperAI>().team = false;
                temp.GetComponent<CTFCamperAI>().charType = 3;
                temp.tag = "BlueTeam";
                temp.GetComponentInChildren<Renderer>().material = BlueDemigod;
                blueTeam.Add(temp);
            }
            
            //Monster Spawns
            if (monSpawns.Count < maxNumMonsterSpawns)
            {
                Transform temp = Instantiate(monSpawnPrefab, new Vector3(Random.Range(rRL, bLL), 25, Random.Range(zFL, zBL)), monSpawnRotation);
                temp.name = "MonSpawn";
                monSpawns.Add(temp);
            }
        }
    }

    public void gameWon(bool team)//True = Red Win, False = Blue Win
    {
        if(GameObject.Find("CTF Guy").GetComponent<CaptureTheFlagMenu>().side == true)//If Player is on Red Team
        {
            if (team)//Red Tean Wins - Player Wins
            {
                (redSide.GetComponent("OnEnableNarrationTrigger") as MonoBehaviour).enabled = true;
                (redSide.GetComponent("OnEnableNarrationTrigger") as MonoBehaviour).enabled = false;
                print("Red Team Wins - Player Wins");

                //If this is first game won then check off the objective from objectives list.
                //Player.GetComponent(<ProgressManager).wonCTF.completed = true;
            }
            else//Blue Team Wins - Player Loses
            {
                (blueSide.GetComponent("OnEnableNarrationTrigger") as MonoBehaviour).enabled = true;
                (blueSide.GetComponent("OnEnableNarrationTrigger") as MonoBehaviour).enabled = false;
                print("Blue Team Wins - Player Loses");
            }
        }
        else//If Player is on Blue Team
        {
            if (team == false)//Blue Team Wins - Player Wins
            {
                (blueSide.GetComponent("OnEnableNarrationTrigger") as MonoBehaviour).enabled = true;
                (blueSide.GetComponent("OnEnableNarrationTrigger") as MonoBehaviour).enabled = false;
                print("Blue Team Wins - Player Wins");

                //If this is first game won then check off the objective from objectives list.
                //Player.GetComponent(<ProgressManager).wonCTF.completed = true;
            }
            else//Red Team Wins - Player Loses
            {
                (redSide.GetComponent("OnEnableNarrationTrigger") as MonoBehaviour).enabled = true;
                (redSide.GetComponent("OnEnableNarrationTrigger") as MonoBehaviour).enabled = false;
                print("Red Team Wins - Player Loses");
            }
        }
        
        playingCTF = false;
        Destroy(redFlag.gameObject);
        Destroy(blueFlag.gameObject);
        blueTeam.Clear();
        redTeam.Clear();
        (gameObject.GetComponent("CaptureTheFlag") as MonoBehaviour).enabled = false;
        Player.transform.position = new Vector3(330, 20, 220);
        Player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        border.SetActive(false);
    }
}