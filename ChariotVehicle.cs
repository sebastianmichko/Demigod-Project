using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class ChariotVehicle : MonoBehaviour
{
    public GameObject Player;
    public ThirdPersonCharacter con1;
    public ThirdPersonUserControl con2;

    public Camera playerCamera;
    public Camera vehicleCamera;
    public Transform exitPositionMarker;//Assigned in Inspector
    public Transform playerPositionMarker;//Assigned in Inspector

    private Rigidbody vehicleRigidbody;

    //Movement Variables
    public float minSpeed;
    public float maxSpeed;
    public float maxReverseSpeed;
    public float acceleration;
    public float maxTurnSpeed;

    public bool controllingVechicle;

    void Start()
    {
        Player = GameObject.Find("ThirdPersonController");
        playerCamera = Player.GetComponentInChildren<Camera>();
        vehicleRigidbody = gameObject.GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        EnterVehicle();
    }

    void Update()
    {
        if (Time.timeScale == 1)
        {
            if (controllingVechicle)//Done in each individual vehicle script
            {
                //Exiting a Vehicle
                if (Input.GetKeyUp("y"))
                {
                    ExitVehicle();
                }
                //print("x Speed: " + vehicleRigidbody.velocity.x);
                //print("y Speed: " + vehicleRigidbody.velocity.y);
                //print("z Speed: " + vehicleRigidbody.velocity.z);
                //Forward/Backward Movement
                if (Input.GetKey("w") && (vehicleRigidbody.velocity.z < maxSpeed)) vehicleRigidbody.AddRelativeForce(0, 0, acceleration * 10000);//(Need to make only only z axis)
                if (Input.GetKey("s") && (vehicleRigidbody.velocity.z > -maxReverseSpeed)) vehicleRigidbody.AddRelativeForce(0, 0, -acceleration * 10000);//Maybe absolute value?

                //Turning Movement
                if (Input.GetKey("a")) vehicleRigidbody.AddTorque(0, -20000, 0);
                if (Input.GetKey("d")) vehicleRigidbody.AddTorque(0, 20000, 0);
                //if (Input.GetKey("a") && (vehicleRigidbody.angularVelocity.y < maxTurnSpeed)) vehicleRigidbody.AddRelativeTorque(0, -20000, 0);
                //if (Input.GetKey("d") && (vehicleRigidbody.angularVelocity.y < maxTurnSpeed)) vehicleRigidbody.AddRelativeTorque(0, 20000, 0);

                /*//Deceleration
                if (!Input.GetKeyDown("w") && !Input.GetKeyDown("s"))
                {
                    if(vehicleRigidbody.velocity.x > 1 || vehicleRigidbody.velocity.y > 1 || vehicleRigidbody.velocity.z > 1)
                    {
                        vehicleRigidbody.velocity -= new Vector3(vehicleRigidbody.velocity.x - 1, vehicleRigidbody.velocity.y - 1, vehicleRigidbody.velocity.z - 1);// -= acceleration;
                    }
                }*/
            }
        }
    }

    public void EnterVehicle()
    {
        Player.transform.position = playerPositionMarker.position;
        Player.transform.rotation = playerPositionMarker.rotation;
        Player.GetComponent<Rigidbody>().velocity = gameObject.GetComponent<Rigidbody>().velocity;
        Player.transform.SetParent(gameObject.transform);
        //playerCamera.enabled = false;
        //vehicleCamera.enabled = true;
        con1.enabled = false;//Something to disable Character Controller Script;
        con2.enabled = false;
        controllingVechicle = true;

        //Adds Player Position and Rotation Constraints
        /*Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX;
        Player.GetComponent<Rigidbody>().constraints &= RigidbodyConstraints.FreezeRotationY;
        Player.GetComponent<Rigidbody>().constraints &= RigidbodyConstraints.FreezeRotationZ;
        Player.GetComponent<Rigidbody>().constraints &= RigidbodyConstraints.FreezePositionX;
        Player.GetComponent<Rigidbody>().constraints &= RigidbodyConstraints.FreezePositionY;
        Player.GetComponent<Rigidbody>().constraints &= RigidbodyConstraints.FreezePositionZ;*/
        Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;

        Player.GetComponent<Rigidbody>().isKinematic = true;
    }

    public void ExitVehicle()
    {
        Player.transform.position = exitPositionMarker.position;
        Player.transform.rotation = exitPositionMarker.rotation;
        Player.GetComponent<Rigidbody>().velocity = gameObject.GetComponent<Rigidbody>().velocity;
        Player.transform.SetParent(Player.transform);
        //playerCamera.enabled = true;
        //vehicleCamera.enabled = false;
        con1.enabled = true;//Something to disable Character Controller Script;
        con2.enabled = true;
        controllingVechicle = false;
        gameObject.GetComponent<ChariotVehicle>().enabled = false;

        //Adds Player Position and Rotation Constraints
        Player.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionX;
        Player.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
        Player.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionZ;

        Player.GetComponent<Rigidbody>().isKinematic = false;
    }
}