#pragma strict

private var Player : GameObject; 

var explosionPrefab : Transform;
var expRotation : Quaternion;

var smokePrefab : Transform;

var arch : GameObject;
var archHole : GameObject;
var platform : GameObject;
private var shot : boolean = false;

var speed : int = 100;

function Start () 
{
    Player = GameObject.Find("ThirdPersonController");
    arch = GameObject.Find("ARCH");
    archHole = GameObject.Find("ARCHHole");
    platform = GameObject.Find("ArchPlatform");
}

function Update () 
{
    if(GameObject.Find("Chimera") == null && !shot)
    {
        shot = true;
        arch.SetActive(false);
        archHole.SetActive(true);
        platform.SetActive(false);
        Player.transform.position = Vector3(245, 316, 327);
        Instantiate(explosionPrefab, Player.transform.position, expRotation);//Bang
        Instantiate(smokePrefab, Player.transform.position, expRotation);//Smoke
        Player.transform.eulerAngles = Vector3(0, 180, 0);
        Player.GetComponent.<Rigidbody>().AddForce(transform.right* speed * 1000);
    }
}
