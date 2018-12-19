//#pragma strict

private var Player: GameObject;
//var interactDistance: float = 3;
var interactLabel : Texture2D;//the Press E to Interact Label
var close : boolean = false;
var fresh : boolean = true;
var enableScript : MonoBehaviour;
var deactAfter : boolean = true;//True deactivates after leaving area, False does not deactive afterward

function OnGUI()
{
    if(close && fresh)
    {
        GUI.Label (Rect (0, 0, interactLabel.width, interactLabel.height),interactLabel);
    }
    
}

function Start () 
{
    Player = GameObject.Find("ThirdPersonController");
    if(gameObject.GetComponent("OnEnableNarrationTrigger") != null && enableScript == null) 
        enableScript = gameObject.GetComponent("OnEnableNarrationTrigger");
}
/*
function Update () 
{
    var dist = Vector3.Distance(Player.transform.position, transform.position);
    if(dist < interactDistance)//Close enough to Character
    {
        close = true;
        if(Input.GetKeyUp("e")) fresh = false;
    }
    else if(dist > interactDistance)
    {
        close = false;
        fresh = true;
    }
}*/

function OnTriggerStay(col : Collider)
{
    if(col.gameObject.name == "ThirdPersonController")
    {
        close = true;
        if(Input.GetKeyUp("e"))
        {
            fresh = false;
            //gameObject.GetComponent("OnEnableNarrationTrigger").enabled = true;
            enableScript.enabled = true;
        }
    }
}
function OnTriggerExit(col : Collider)
{
    if(col.gameObject.name == "ThirdPersonController")
    {
        close = false;
        fresh = true;
        //gameObject.GetComponent("OnEnableNarrationTrigger").enabled = false;
        if(deactAfter == true) enableScript.enabled = false;
    }
}