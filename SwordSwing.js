#pragma strict

//var User: GameObject;
var finish : Vector3;

function Start () 
{
    //User = GameObject.Find("ThirdPersonController");
    finish = transform.position + (transform.up * 2) /*+ (transform.up)+ (transform.forward*0.5)*/;
}

function Update () 
{
    //transform.position = Vector3.Slerp(transform.position, User.transform.position-(User.transform.right) + (User.transform.up)+ (User.transform.forward*0.5), 0.5f);
    transform.position = Vector3.Slerp(transform.position, finish, 0.5f);
}