#pragma strict

//var User: GameObject;
//private var finish : Vector3;
var maxLength : float = 1.5;
var done : boolean = false;
var speed : float = 100;

function Start () 
{
    //User = GameObject.Find("ThirdPersonController");
    //finish = transform.position + transform.forward;
}

function Update () 
{
    //transform.position = Vector3.Slerp(transform.position, User.transform.position-(User.transform.right) + (User.transform.up)+ (User.transform.forward*0.5), 0.5f);
    //transform.position = Vector3.Slerp(transform.position, finish, 0.5f);

    if(transform.localScale.z < maxLength && !done)
    {
        transform.localScale += new Vector3(0, 0, 0.05) * Time.deltaTime * speed;
    }
    else if (transform.localScale.z >= 0.05)
    {
        transform.localScale -= new Vector3(0, 0, 0.05) * Time.deltaTime * speed;
        done = true;
    }
    var rend: Renderer = GetComponent.<Renderer>();
    var col: Collider = GetComponent.<Collider>();
    if(done && transform.localScale.z < 0.05) 
    {
        rend.enabled = false;
        col.enabled = false;
    }
    //transform.localScale += new Vector3(0, 0, 1) * Time.deltaTime * growFactor;
    //transform.localScale -= new Vector3(0, 0, 1) * Time.deltaTime * growFactor;
}