using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swimming : MonoBehaviour {

    private Rigidbody rigidBody;
    public Component cam;//Has to be assigned in Unity for now
    public bool canSwim = true;
    public bool inWater = false;
    private bool touchingLand = false;
    public float swimSpeed = 2.0f;
    public float turnSpeed = 1.5f;
    //public float swimHeight = 18.65f;//Make it Get once it Hits Water Surface to allow Different leveled Swiming in different heights EX: FOuntain and ocean in same scene
    public float waterSurfaceHeight = 18.65f;
    private float maxSwimHeight;//Water Swim Trigger should be 1.??? meters below maxSwimHeight
    public int waterEnExAngle = 40;
    public float waterDrag = 2.0f;
    //var atSurface : boolean = true;

/*
    void OnTriggerEnter(Collider col)//Entering Water
    {
        if (col.gameObject.name == "Water Swim Trigger" && (inWater == false) && canSwim)
        {
            inWater = true;
            (Player.GetComponent("ThirdPersonCharacter") as MonoBehaviour).enabled = false;
            (Player.GetComponent("ThirdPersonUserControl") as MonoBehaviour).enabled = false;
            playerRB.drag = waterDrag;
            playerRB.useGravity = false;
            //gameObject.transform.eulerAngles.x = 40;//Rotates the player to face the water
            gameObject.transform.rotation = Quaternion.Euler(40, transform.rotation.y, transform.rotation.z);
            gameObject.transform.position = new Vector3(transform.position.x, swimHeight, transform.position.z);

            //Rigid body constraints
            playerRB.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;// | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
        }
    }

    function OnCollisionEnter (col : Collision)//Leaving Water
    {
        if(col.gameObject.name == "Terrain" && inWater && Player.transform.position.y >= swimHeight  && canSwim)
        {
            print("OnCOllEnter");
            inWater = false;
            (Player.GetComponent( "ThirdPersonCharacter" ) as MonoBehaviour).enabled = true;
            (Player.GetComponent( "ThirdPersonUserControl" ) as MonoBehaviour).enabled = true;
            playerRB.drag = 0;
            playerRB.useGravity = true;
            Player.transform.eulerAngles.x = 0;//Rotates the player to face forward
            Player.transform.position.y = 21;
            Player.transform.Translate(0,0,2, Space.Self);
            playerRB.velocity = Vector3.zero;//cancels force of swimming

            //Rigidbody constraints
            playerRB.constraints &= ~RigidbodyConstraints.FreezePositionX;
            playerRB.constraints &= ~RigidbodyConstraints.FreezePositionY;
            playerRB.constraints &= ~RigidbodyConstraints.FreezePositionZ;
            playerRB.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }
    }

    //Also leaving Water
    void OnTriggerExit(Collider col)//Breaks because it deals with Damage Area Trigger
    {
        if (col.gameObject.name == "Water Swim Trigger" && canSwim && inWater)
        {
            inWater = false;
            playerRB.useGravity = true;
        }
    }
    */
    void Start ()
    {
        rigidBody = gameObject.GetComponent<Rigidbody> ();
        maxSwimHeight = waterSurfaceHeight - 0.8f;
    }
	
	void Update ()
    {
        if (inWater && Time.timeScale == 1 && canSwim)
        {
            //Backward
            if (Input.GetKey("s"))
            {
                rigidBody.AddRelativeForce(new Vector3(0, -1, -(1 / Mathf.Tan(0.698132f))) * swimSpeed * 50);//Cheap Fix----------------------------------------
            }
            //Forward
            if (Input.GetKey("w"))
            {
                rigidBody.AddRelativeForce(new Vector3(0, 1, (1 / Mathf.Tan(0.698132f))) * swimSpeed * 100);//Cheap Fix----------------------------------------
            }
            //Left Turn
            if (Input.GetKey("a"))
            {
                gameObject.transform.Rotate(new Vector3(0, -turnSpeed, 0), Space.World);
            }
            //Right Turn
            if (Input.GetKey("d"))
            {
                gameObject.transform.Rotate(new Vector3(0, turnSpeed, 0), Space.World);
            }
            //Surface
            if (Input.GetKey("e"))
            {
                if (gameObject.transform.position.y > maxSwimHeight)
                {
                    rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0, rigidBody.velocity.z);
                }
                else
                {
                    rigidBody.AddForce(Vector3.up * swimSpeed * 50);
                }
            }
            //Dive
            if (Input.GetKey("q"))
            {
                rigidBody.AddForce(Vector3.up * -swimSpeed * 50);
            }
        }
    }

    void OnCollisionEnter(Collision col)//Touching Ground
    {
        if (col.gameObject.name == "Terrain")
        {
            touchingLand = true;
            //print("Touching Land");
        }
    }

    void OnCollisionExit(Collision col)//Leaving Ground
    {
        if (col.gameObject.name == "Terrain")
        {
            touchingLand = false;
            //print("No longer Touching Land");
        }
    }

    public void whatToDo(bool IW)
    {
        inWater = IW;
        if(inWater)//Entered Water
        {
            //print("Entered Water");
            (gameObject.GetComponent("ThirdPersonCharacter") as MonoBehaviour).enabled = false;//Won't work for other NPCS -> IF Statement for if it's player
            (gameObject.GetComponent("ThirdPersonUserControl") as MonoBehaviour).enabled = false;
            rigidBody.drag = waterDrag;
            rigidBody.useGravity = false;
            //gameObject.transform.eulerAngles.x = 40;//Rotates the player to face the water
            //print("X: " + transform.eulerAngles.x);
            //print("Y: " + transform.eulerAngles.y);
            //print("Z: " + transform.eulerAngles.z);
            gameObject.transform.rotation = Quaternion.Euler(40, transform.eulerAngles.y, transform.eulerAngles.z);
            gameObject.transform.position = new Vector3(transform.position.x, maxSwimHeight, transform.position.z);

            //Rigid body constraints
            rigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;// | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
        }
        else//Leaving Water
        {
            //print("Left Water");
            (gameObject.GetComponent("ThirdPersonCharacter") as MonoBehaviour).enabled = true;
            (gameObject.GetComponent("ThirdPersonUserControl") as MonoBehaviour).enabled = true;
            rigidBody.drag = 0;
            rigidBody.useGravity = true;
            gameObject.transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, transform.eulerAngles.z);

            if(touchingLand)//Hoping to Ground __________Insead of this add force vertically Up when swimming into land ______ Or just make all water have slopping coast
            {
                //print("Hopping to Land");
                gameObject.transform.position = new Vector3(transform.position.x, waterSurfaceHeight + 1, transform.position.z);
                gameObject.transform.Translate(0, 0, 2, Space.Self);
            }
        
            rigidBody.velocity = Vector3.zero;//cancels force of swimming

            //Rigidbody constraints
            rigidBody.constraints &= ~RigidbodyConstraints.FreezePositionX;
            rigidBody.constraints &= ~RigidbodyConstraints.FreezePositionY;
            rigidBody.constraints &= ~RigidbodyConstraints.FreezePositionZ;
            rigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }
    }
}