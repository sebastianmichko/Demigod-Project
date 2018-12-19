using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Narrate;

public class AntHill : MonoBehaviour
{

    public Transform ant;
    public int maxAnt = 3;
    private int curAnt = 0;
    private Quaternion antRotation;
    public OnEnableNarrationTrigger script;

    void Start()
    {
        antRotation.eulerAngles = new Vector3(0, 0, 0);
        script = gameObject.GetComponent<OnEnableNarrationTrigger>();
    }

    //function OnCollisionEnter (col : Collision)
    void OnTriggerEnter(Collider col)
    {
        //print(col.gameObject.name);
        if (col.gameObject.tag == "RedTeam" || col.gameObject.tag == "BlueTeam" || col.gameObject.tag == "Demigod")
        {
            if (col.gameObject.name == "ThirdPersonController")
            {
                script.enabled = true;
                //script.enabled = false;
            }
            while (curAnt < maxAnt)
            {
                //gameObject.GetComponent(SphereCollider).isTrigger = false;
                Instantiate(ant, new Vector3(transform.localPosition.x + (curAnt * 2), transform.position.y + 1, transform.localPosition.z), antRotation);
                ++curAnt;
            }
            Destroy(gameObject, 5);
        }
    }
}