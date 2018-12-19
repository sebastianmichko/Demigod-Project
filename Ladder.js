#pragma strict

var goUp: boolean;
var height: float;
private var close : boolean = false;
private var Player: GameObject; 
private var currentSentence: String;
var interactDistance: float;
var interactLabel : Texture2D;//the Press E to Interact Label
//Add E control and make the rockwall distinguish between the ladder E press and the RW E press.  Also Move ladder to back of RW


function OnGUI()
{
    if(close == true)
    {
        GUI.Label (Rect (0, 0, interactLabel.width, interactLabel.height),interactLabel);
    }
    GUI.Label(Rect(0,50,500,100),currentSentence);
}

function Start () 
{
    Player = GameObject.Find("ThirdPersonController");	
}

function Update () 
{
    var dist = Vector3.Distance(Player.transform.position, transform.position);	

    //controls close variable
    if(dist < interactDistance)
    {
        close = true;
    }
    else
    {
        close = false;
    }

    if(Input.GetKeyUp("e") && close && goUp)//Go Up
    {
        Player.transform.position = Vector3(transform.position.x - 2, height, transform.position.z );
        Player.transform.rotation = Quaternion.Euler(0, 270, 0);
    }
    else if(Input.GetKeyUp("e") && close && goUp == false)//Go DOwn
    {
        Player.transform.position = Vector3(transform.position.x + 3, 20, transform.position.z );
        Player.transform.rotation = Quaternion.Euler(0, 90, 0);
    }
}