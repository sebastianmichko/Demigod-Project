#pragma strict

var Player: GameObject;  
var Canoe: GameObject;
private var currentSentence: String;
//var interactDistance: float = 5;
var interactLabel : Texture2D;//the Press E to Interact Label
//var close : Boolean = false;
var inCanoe: boolean = false;
var swimScript : MonoBehaviour;

var swimming : boolean;

//Movement Variables
var maxSpeed: float = 1;
var speed: float = 0;
var turnspeed: float = 30;
var rudderAngle: float = 0;
var maxRudderAngle: float = 30;



function OnGUI()
{
    /*if(close == true && inCanoe == false)
    {
        GUI.Label (Rect (0, 0, interactLabel.width, interactLabel.height),interactLabel);
    }
    GUI.Label(Rect(0,50,500,100),currentSentence);
    */
}

function Start () 
{
    Player = GameObject.Find("ThirdPersonController");
    swimScript = (Player.GetComponent( "Swimming" ) as MonoBehaviour);

}


function Update () 
{
        if(inCanoe == false)
        {
            Player.transform.position = transform.position;
            Player.transform.rotation = transform.rotation;
            Player.transform.SetParent(Canoe.transform);
            inCanoe = true;

            //Disable Third Person Controller
            (Player.GetComponent( "ThirdPersonCharacter" ) as MonoBehaviour).enabled = false;
            (Player.GetComponent( "ThirdPersonUserControl" ) as MonoBehaviour).enabled = false;

            //Adds Player Position and Rotation Constraints
            Player.GetComponent.<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
        }
        else if(Input.GetKeyUp("e") && inCanoe == true)
        {
            Player.transform.parent = null;
            inCanoe = false;

            //Re-enables Third Person Controller
            (Player.GetComponent( "ThirdPersonCharacter" ) as MonoBehaviour).enabled = true;
            (Player.GetComponent( "ThirdPersonUserControl" ) as MonoBehaviour).enabled = true;

            //Adds Player Position and Rotation Constraints
            Player.GetComponent.<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionX;
            Player.GetComponent.<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
            Player.GetComponent.<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionZ;
            (Canoe.GetComponent( "Canoe" ) as MonoBehaviour).enabled = false;
        }
    //}
    /*
    else if(dist > interactDistance)
    {
        //close = false;
    }*/
    
    //Movement Code
    if(inCanoe && Time.timeScale == 1)
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
        Canoe.GetComponent.<Rigidbody>().AddForce(transform.forward * speed * 100);
        




        //Left Turn
        if ( ( Input.GetKey("a") == true ) && ( Input.GetKey("d") == false) )
        {
            Canoe.transform.Rotate(-Vector3.up,1);
        }

        //Right Turn
        if ( ( Input.GetKey("d") == true ) && ( Input.GetKey("a") == false) ) 
        {
            Canoe.transform.Rotate(Vector3.up,1);
        }
    }
}