#pragma strict

private var Player: GameObject;
private var currentSentence: String;
var interactDistance: float = 3;
var interactLabel : Texture2D;//the Press E to Interact Label
private var close : boolean = false;
var text : String;
var sound : AudioClip; //canon fire soundClip
/*
function OnGUI()
{
    if(close)
    {
        GUI.Label (Rect (0, 0, interactLabel.width, interactLabel.height),interactLabel);
    }
    GUI.Label(Rect(0,50,500,100),currentSentence);
}
*/
function Start () 
{
    Player = GameObject.Find("ThirdPersonController");
    AudioSource.PlayClipAtPoint(sound, transform.position, 1);// object's Z axis




}
/*
function Update () 
{
    var dist = Vector3.Distance(Player.transform.position, transform.position);
    //print ("Distance to Player: " + dist);
    if(dist < interactDistance)//Close enough to Character
    {
        close = true;
        if(Input.GetKeyUp("e"))
        {
            currentSentence = text;//Window
            AudioSource.PlayClipAtPoint(sound, transform.position, 1);// object's Z axis
        }
    }
    else if(dist > interactDistance)
    {
        currentSentence = "";
        close = false;
    }
}*/