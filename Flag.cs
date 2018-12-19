using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour {

    private bool close = false;
    public int held = 0;//0 = Not Held, 1 = Player Held, 2 = NPC Held
    private GameObject Player;
    public Texture2D interactLabel;//the Press E to Interact Label
    CaptureTheFlag ctfScript;
    public bool redTeam;
    public GameObject banner;
    private Rigidbody rigidBody;

    void OnGUI()
    {
        if (close == true && held == 1)
        {
            GUI.Label(new Rect(0, 0, interactLabel.width, interactLabel.height), interactLabel);
        }
    }

    void Start ()
    {
        rigidBody = gameObject.GetComponent<Rigidbody>();
        Player = GameObject.Find("ThirdPersonController");
    }
	
	void Update ()
    {
        if (Input.GetKeyUp("e") && close)
        {
            if (held == 0)//Grabs Guideon
            {
                transform.SetParent(Player.transform);
                gameObject.GetComponent<Rigidbody>().useGravity = false;
                held = 1;
                
                //Rigid body constraints
                rigidBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            }
            else//Drops Guideon
            {
                transform.parent = null;
                gameObject.GetComponent<Rigidbody>().useGravity = true;
                held = 0;
                
                //Rigidbody constraints
                rigidBody.constraints &= ~RigidbodyConstraints.FreezePositionX;
                rigidBody.constraints &= ~RigidbodyConstraints.FreezePositionY;
                rigidBody.constraints &= ~RigidbodyConstraints.FreezePositionZ;
            }
        }
    }

    //Triggers
    void OnTriggerEnter(Collider col)
    {
        if (redTeam)//Red Team's Flag
        {
            if (col.gameObject.name == "BlueTeamDemigod" && held == 0)
            {
                transform.SetParent(col.transform);
                held = 2;
                gameObject.GetComponent<Rigidbody>().useGravity = false;
                col.gameObject.GetComponent<CTFCamperAI>().hasGuideon = true;
                (gameObject.GetComponent("OnEnableNarrationTrigger") as MonoBehaviour).enabled = true;
                (gameObject.GetComponent("OnEnableNarrationTrigger") as MonoBehaviour).enabled = false;
            }
            if (col.gameObject.name == "blueSideCTF" && held != 0)
            {
                print("Red Team's Guideon touched the blue side");
                GameObject.Find("Capture the Flag").GetComponent<CaptureTheFlag>().gameWon(false);
            }
        }
        else//Blue Team's Flag
        {
            if (col.gameObject.name == "RedTeamDemigod" && held == 0)
            {
                transform.SetParent(col.transform);
                held = 2;
                gameObject.GetComponent<Rigidbody>().useGravity = false;
                col.gameObject.GetComponent<CTFCamperAI>().hasGuideon = true;
                (gameObject.GetComponent("OnEnableNarrationTrigger") as MonoBehaviour).enabled = true;
                (gameObject.GetComponent("OnEnableNarrationTrigger") as MonoBehaviour).enabled = false;
            }
            if (col.gameObject.name == "redSideCTF" && held != 0)
            {
                print("Blue Team's Guideon touched the red side");
                GameObject.Find("Capture the Flag").GetComponent<CaptureTheFlag>().gameWon(true);
            }
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.name == "ThirdPersonController")//Maybe add an option in the future to Take the guideon from your team mates
        {
            close = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "ThirdPersonController")
        {
            close = false;
        }
    }
}