using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockWall : MonoBehaviour
{
    private GameObject Player; 
    public bool onWall = false;
    private float xLL;
    private float xRL;

    public Transform lavaPrefab;
    public int maxLava = 8;
    private Quaternion lavaRotation;
    public float lavaMinSpeed = 0.001f;
    public float lavaMaxSpeed = 0.01f;

    public Transform spikePrefab;
    public int maxSpikes = 10;
    private Quaternion spikeRotation;

    public List<Transform> lava = new List<Transform>();
    public List<Transform> spikes = new List<Transform>();

    //Movement Variables
    public float climbSpeed = 0.075f;

	void Start ()
    {
        Player = GameObject.Find("ThirdPersonController");
        xRL = (transform.position.x - (transform.localScale.x / 2) + 1);
        xLL = (transform.position.x + (transform.localScale.x / 2) - 1);
        lavaRotation.eulerAngles = new Vector3(-90, 0, 0);
        spikeRotation.eulerAngles = new Vector3(90, 0, 0);

        /*
        //Lava    
        tempX = Random.Range( xLL, xRL );
        for (var j : int = 0; j < maxLava; ++j)//Spawns Lava
        {
            for (var i : int = 0; i < maxLava; ++i)//Checks each past lava
            {
                //print("Temp X: " + tempX);
                //print("Lava position.x: " + lava[i].position.x);
                //print("Test " + (tempX - lava[i].position.x));
                //if(i != 0)
                //{
                    while( Mathf.Abs(tempX - lava[i].position.x) < 3 )
                    {
                        tempX = Random.Range( xLL, xRL );
                        print("Re did Number " + tempX);
                    }
                //}
            }
            lava[j] = Instantiate(lavaPrefab, Vector3 (tempX, 35, (transform.position.z + (transform.localScale.z / 2)) ), lavaRotation);
            print("Actually Spawned Temp X: " + tempX);
            tempX = Random.Range( xLL, xRL );
            //lava[curLava] = Instantiate(lavaPrefab, Vector3 (Random.Range( xLL, xRL ), 35, (transform.position.z + (transform.localScale.z / 2)) ), lavaRotation);
            //curLava = curLava + 1;
            //-----------------------Problem is It only checks the last one not alll already spawned lavas
        }*/
    }
    private void OnEnable()
    {
        if (!Player) Player = GameObject.Find("ThirdPersonController");
        xRL = (transform.position.x - (transform.localScale.x / 2) + 1);
        xLL = (transform.position.x + (transform.localScale.x / 2) - 1);
        lavaRotation.eulerAngles = new Vector3(-90, 0, 0);
        spikeRotation.eulerAngles = new Vector3(90, 0, 0);

        Player.transform.position = new Vector3(transform.position.x, 20, (transform.position.z + (transform.localScale.z / 2) + 0.25f));
        Player.transform.rotation = Quaternion.Euler(0, 180, 0);
        onWall = true;

        //Disable Third Person Controller
        (Player.GetComponent("ThirdPersonCharacter") as MonoBehaviour).enabled = false;
        (Player.GetComponent("ThirdPersonUserControl") as MonoBehaviour).enabled = false;

        //Adds Player Position and Rotation Constraints
        Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;

        //Spikes
        while (spikes.Count < maxSpikes)
        {
            spikes.Add(Instantiate(spikePrefab, new Vector3(Random.Range(xLL - 0.5f, xRL + 0.5f), Random.Range(22, 34.25f), (transform.position.z + (transform.localScale.z / 2))), spikeRotation));
        }
        //lava
        while (lava.Count < maxLava)
        {
            lava.Add(Instantiate(lavaPrefab, new Vector3(Random.Range(xLL - 0.5f, xRL + 0.5f), 35, (transform.position.z + (transform.localScale.z / 2))), lavaRotation));
        }
    }

    void Update ()
    {
        if (Input.GetKeyUp("y") && onWall == true)
        {
            Player.transform.position = new Vector3(transform.position.x, 20, transform.position.z + 4);
            endGame();
        }

        //Movement Code
        if (onWall && Time.timeScale == 1)
        {
            if (Input.GetKey("s") && (Player.GetComponent<Rigidbody>().transform.position.y > 20))
            {
                Player.GetComponent<Rigidbody>().transform.Translate(0, -climbSpeed, 0);
            }

            //Backward
            if (Input.GetKey("w") && ((Player.GetComponent<Rigidbody>().transform.position.y - 20) < transform.localScale.y))
            {
                Player.GetComponent<Rigidbody>().transform.Translate(0, climbSpeed, 0);
            }

            //Left Turn
            if ((Input.GetKey("a") == true) && (Player.GetComponent<Rigidbody>().transform.position.x < (transform.position.x + (transform.localScale.x / 2) - 0.25)))
            {
                Player.GetComponent<Rigidbody>().transform.Translate(-climbSpeed, 0, 0);
            }

            //Right Turn
            if ((Input.GetKey("d") == true) && (Player.GetComponent<Rigidbody>().transform.position.x > (transform.position.x - (transform.localScale.x / 2) + 0.25)))
            {
                Player.GetComponent<Rigidbody>().transform.Translate(climbSpeed, 0, 0);
            }
        }
        //Winning Script
        if (onWall && ((Player.GetComponent<Rigidbody>().transform.position.y - 20) > transform.localScale.y))
        {
            (gameObject.GetComponent("OnEnableNarrationTrigger") as MonoBehaviour).enabled = true;
            (gameObject.GetComponent("OnEnableNarrationTrigger") as MonoBehaviour).enabled = false;
            Player.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z - 2);

            endGame();
        } 
    }

    void despawnAll()
    {
        foreach (var Transform in lava)
        {
            Destroy(Transform.gameObject);
        }

        foreach (var Transform in spikes)
        {
            Destroy(Transform.gameObject);
        }
    }

    void endGame()
    {
        onWall = false;

        //Re-enables Third Person Controller
        (Player.GetComponent("ThirdPersonCharacter") as MonoBehaviour).enabled = true;
        (Player.GetComponent("ThirdPersonUserControl") as MonoBehaviour).enabled = true;

        //Adds Player Position and Rotation Constraints
        Player.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionX;
        Player.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
        Player.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionZ;
        gameObject.GetComponent<RockWall>().enabled = false;

        despawnAll();
        lava.Clear();
        spikes.Clear();
        Player.GetComponent<PlayerHealth>().curHealth = 100;
        GameObject.Find("ThirdPersonController").GetComponent<PlayerSwordAttack>().meleeBlock = false;
    }
}