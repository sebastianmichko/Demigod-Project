#pragma strict

var speed : float = 50;

function Start () 
{
    //xPos = transform.position.x;
    //zPos = transform.position.z;
}

function Update () 
{
    gameObject.GetComponent.<Rigidbody>().AddForce(transform.up * speed * 100);
}

//Triggers
/*function OnTriggerEnter(col : Collider)
{

}

function OnTriggerStay(col : Collider)
{

}

function OnTriggerExit(col : Collider)
{

}*/

//Collisions
function OnCollisionEnter(col : Collision)
{
        transform.rotation.eulerAngles = transform.rotation.eulerAngles + Vector3(0, Random.Range( 90, 270 ), 0);
}

/*function OnCollisionStay(col : Collision)
{

}

function OnCollisionExit(col : Collision)
{

}*/