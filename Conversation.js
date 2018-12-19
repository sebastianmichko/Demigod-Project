#pragma strict

private var Player: GameObject;
var characterName : String = "Camper";
private var currentSentence: String = "";
var dialog = ["", "", ""];
var interactDistance: float = 3;
var interactLabel : Texture2D;//the Press E to Interact Label
private var close : boolean = false;
private var curSentence : int = 0;

function OnGUI()
{
    if(close)
    {
        GUI.Label (Rect (0, 0, interactLabel.width, interactLabel.height),interactLabel);
        if(currentSentence != "") GUI.Label(Rect(0,50,500,100),characterName + ": " + currentSentence);
    }
    
}

function Start () 
{
    Player = GameObject.Find("ThirdPersonController");
}

function Update () 
{
    var dist = Vector3.Distance(Player.transform.position, transform.position);
    if(dist < interactDistance)//Close enough to Character
    {
        close = true;
        if(Input.GetKeyUp("e"))
        {
            currentSentence = dialog[curSentence];//Window
            if( curSentence < (dialog.length - 1) ) ++curSentence;
            else curSentence = 0;
        }
    }
    else if(dist > interactDistance)
    {
        currentSentence = "";
        close = false;
        curSentence = 0;
    }
}