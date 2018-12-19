#pragma strict

var Player : Rigidbody;
var water: GameObject;

function Start () 
{

}

function Update () 
{
	
}

function OnCollisionEnter (col : Collision)
    {
        if(col.gameObject.name == "ThirdPersonController")
        {
            Player.useGravity = false;
            //water.gameObject.BoxCollider.SetActive(false);
            Player.position = Vector3(transform.position.x, 18, Player.transform.position.z);
        }
    }
    function OnCollisionExit (col : Collision)
        {
            if(col.gameObject.name == "ThirdPersonController")
            {
                Player.useGravity = true;
            }
        }