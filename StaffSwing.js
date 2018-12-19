#pragma strict

private var finish : Vector3;
public var timeForSwing : float = 1.0f;

function Start () 
{
    finish = transform.position - (transform.right * 2);
}

function Update () 
{
    //transform.position = Vector3.Slerp(transform.position, User.transform.position-(User.transform.right) + (User.transform.up)+ (User.transform.forward*0.5), 0.5f);
    transform.position = Vector3.Slerp(transform.position, finish, timeForSwing);
}