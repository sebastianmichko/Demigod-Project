#pragma strict

var godLabel : Texture2D;//the banner of the various Gods
var godLabelPrefab : Transform;
private var labelRot: Quaternion; 
var sound : AudioClip; //Claim soundClip

function Start () 
{
    labelRot.eulerAngles = Vector3(90, (transform.eulerAngles.y), 0);
    Instantiate(godLabelPrefab, Vector3 (transform.position.x , transform.position.y + 2.5, transform.position.z ), labelRot);//,transform
    AudioSource.PlayClipAtPoint(sound, transform.position, 1);// object's Z axis
}

function Update () 
{
	
}
