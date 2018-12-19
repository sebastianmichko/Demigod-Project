#pragma strict

var Player: GameObject;  
var Chariot: GameObject;
var inChariot: boolean = false;


//Movement Variables
var maxSpeed: float = 100;
var speed: float = 0;
var turnspeed: float = 15;

function OnGUI()
{
}

function Start () 
{
    Player = GameObject.Find("ThirdPersonController");
}


function Update () 
{
    //Enter/Exit Chariot Code        

        if(inChariot == false)
        {       
            Player.transform.position = transform.position;
            Player.transform.rotation = transform.rotation;
            Player.transform.SetParent(Chariot.transform);
            inChariot = true;

            //Disable Third Person Controller
            (Player.GetComponent( "ThirdPersonCharacter" ) as MonoBehaviour).enabled = false;
            (Player.GetComponent( "ThirdPersonUserControl" ) as MonoBehaviour).enabled = false;

            //Adds Player Position and Rotation Constraints
            Player.GetComponent.<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
        }
        else if(Input.GetKeyUp("e") && inChariot == true)
        {
            Player.transform.parent = null;
            inChariot = false;

            //Re-enables Third Person Controller
            (Player.GetComponent( "ThirdPersonCharacter" ) as MonoBehaviour).enabled = true;
            (Player.GetComponent( "ThirdPersonUserControl" ) as MonoBehaviour).enabled = true;

            //Adds Player Position and Rotation Constraints
            Player.GetComponent.<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionX;
            Player.GetComponent.<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
            Player.GetComponent.<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionZ;

            (Chariot.GetComponent( "Chariot" ) as MonoBehaviour).enabled = false;

        }
    
    //Movement Code
    if(inChariot)
    {
        if (Input.GetKey("s")) if(speed > -maxSpeed)speed = speed - 1;
   
        //Backward
        if (Input.GetKey("w")) if(speed < maxSpeed)speed = speed + 1;

        if(Input.GetKey("w") == false && Input.GetKey("s") == false)
        {
            if(speed < 0)
            {
                speed += 1;
            }
            else if(speed > 0)
            {
                speed -= 1;
            }
        }
    
        //Applying Speed
        Chariot.GetComponent.<Rigidbody>().AddForce(transform.forward * speed * 10000);
        
        //Left Turn
        if ( ( Input.GetKey("a") == true ) && ( Input.GetKey("d") == false) )
        {
            transform.Rotate(-Vector3.up,1);
        }

        //Right Turn
        if ( ( Input.GetKey("d") == true ) && ( Input.GetKey("a") == false) ) 
        {
            transform.Rotate(Vector3.up,1);
        }
    }
}